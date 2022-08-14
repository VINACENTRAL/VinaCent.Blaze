using Abp.Dependency;
using Abp.Domain.Entities;
using System.Threading.Tasks;

namespace VinaCent.Blaze.AppCore.TranslateFields
{
    public interface ITranslateFieldManager<TEntity, TPrimaryKey>: ITransientDependency
        where TEntity : IEntity<TPrimaryKey>
    {
        TranslateField[] GetFields(TEntity entity, string languageName);

        Task SaveFields(params TranslateField[] translatedFields);
    }
}
