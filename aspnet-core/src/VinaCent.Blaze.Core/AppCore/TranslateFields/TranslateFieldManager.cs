using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using System;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.Attributes;

namespace VinaCent.Blaze.AppCore.TranslateFields
{
    public class TranslateFieldManager<TEntity, TPrimaryKey> : ITranslateFieldManager<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<TranslatedField, Guid> _repository;

        public TranslateFieldManager(IRepository<TranslatedField, Guid> repository, IObjectMapper objectMapper)
        {
            _repository = repository;
            _objectMapper = objectMapper;
        }

        public TranslateField[] GetFields(TEntity entity, string languageName)
        {
            return entity.GetType()
                .GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(TranslateFieldAttribute)))
                .Select(x => new TranslateField
                {
                    LanguageName = languageName,
                    EntityName = entity.GetType().Name,
                    EntityId = entity.Id.ToString(),
                    FieldName = x.Name,
                    LanguageText = x.GetValue(entity)?.ToString() ?? ""
                }).ToArray();
        }

        public async Task SaveFields(params TranslateField[] translatedFields)
        {
            var firstField = translatedFields.FirstOrDefault();
            if (firstField == null)
            {
                throw new Exception("Not found any field!");
            }
            await _repository.DeleteAsync(x => x.LanguageName == firstField.LanguageName && x.EntityName == firstField.EntityName && x.EntityId == firstField.EntityId);

            var updateFields = translatedFields.Where(x => !x.IsIgnore);
            foreach (var field in updateFields)
            {
                var entity = _objectMapper.Map<TranslatedField>(field);
                await _repository.InsertAsync(entity);
            }
        }
    }
}
