using System.Collections.Generic;

namespace TestWebAPI.CustomerServicesEvaluation
{
    public class AllAttractionEvaluationPointDto
    {
        public List<AttractionEvaluationDto> AttractionEvaluations { get; set; }
        public float? FinalPropertyEvaluation { get; set; }
    }
}
