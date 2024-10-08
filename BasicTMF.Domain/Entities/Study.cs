using BasicTMF.Domain.Common;


namespace BasicTMF.Domain.Entities
{
    public sealed class Study : Base
    {
        public string ProjectID { get; set; } = default!;

        public string Compound { get; set; } = default!;

        public string Sponsor { get; set; } = default!;
    }
}
