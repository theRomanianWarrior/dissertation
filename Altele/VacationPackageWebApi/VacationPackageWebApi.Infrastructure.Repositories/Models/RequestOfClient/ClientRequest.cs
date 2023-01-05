﻿using System.ComponentModel.DataAnnotations;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient;

public class ClientRequest
{
    [Required] public Guid Id { get; set; }

    [Required] public Guid PreferencesPackageId { get; set; }

    public Guid? RecommendationId { get; set; }
    public Guid? Evaluation { get; set; }

    [Required] public Guid CustomerId { get; set; }

    [Required] public DateTime RequestTimestamp { get; set; }

    public virtual Customer.Customer Customer { get; set; }
    public virtual ServiceEvaluation EvaluationNavigation { get; set; }
    public virtual PreferencesPackage PreferencesPackage { get; set; }
    public virtual Recommendation Recommendation { get; set; }
}