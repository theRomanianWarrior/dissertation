using System;

namespace TestWebAPI;

public record BaseBusinessModel
{
    public Guid StoredInLocalDbOfAgentWithId { get; set; }
};