﻿using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient
{
    public record ClientRequest
    {
        public Guid Id { get; set; }
        public Guid PreferencesPackageId { get; set; }
        public Guid RecommendationId { get; set; }
        public Guid Evaluation { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime RequestTimestamp { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ServiceEvaluation EvaluationNavigation { get; set; }
        public virtual PreferencesPackage PreferencesPackage { get; set; }
        public virtual Recommendation.Recommendation Recommendation { get; set; }
    }
}
