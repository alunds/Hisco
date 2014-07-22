namespace Hisco.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    public class Entry
    {
        [JsonIgnore]
        [XmlIgnore]
        public long Id { get; set; }
        [Required]
        [Range(1, 65535)]
        public ushort Level { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Range(1, 999.999)]
        public decimal Score { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public DateTime Created { get; set; }
    }
}