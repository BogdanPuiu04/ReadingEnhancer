﻿using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Domain.Repositories
{
    public interface IEnhancedTextRepository : IGenericRepository<EnhancedText>
    {
        public Task<EnhancedText> GetRandomAsync();
    }
}