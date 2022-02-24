using Newtonsoft.Json;

namespace HelperLibrary.Models.Base
{
    public abstract class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }
    }
}
