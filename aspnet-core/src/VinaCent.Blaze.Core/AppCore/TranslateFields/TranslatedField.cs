using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.AppCore.TranslateFields
{
    [Table(nameof(AppCore) + "." + nameof(TranslateFields))]
    public class TranslatedField : AuditedEntity<Guid>
    {
        public string LanguageName { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string FieldName { get; set; }
        public string LanguageText { get; set; }
    }
}
