﻿namespace TestWebAPI.PreferencesPackageRequest
{
    public record AgeCategoryPreferenceDto
    {
        public short Adult { get; set; }
        public short Children { get; set; }
        public short Infant { get; set; }
    }
}
