﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Modix.Data.Models.Moderation;

namespace Modix.Data.Repositories
{
    /// <summary>
    /// Describes a repository for managing <see cref="ModerationActionEntity"/> entities, within an underlying data storage provider.
    /// </summary>
    public interface IModerationActionRepository
    {
        /// <summary>
        /// Retrieves information about a moderation action from the repository.
        /// </summary>
        /// <param name="moderationActionId">The <see cref="ModerationActionEntity.Id"/> value of the moderation action to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the operation has completed,
        /// containing the requested moderation action, or null if no such action exists.
        /// </returns>
        Task<ModerationActionSummary> ReadSummaryAsync(long moderationActionId);

        /// <summary>
        /// Searches the repository for moderation action information, based on an arbitrary set of criteria.
        /// </summary>
        /// <param name="searchCriteria">A set of criteria defining the moderation actions to be returned.</param>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the operation has completed,
        /// containing the requested moderation actions.
        /// </returns>
        Task<IReadOnlyCollection<ModerationActionSummary>> SearchSummariesAsync(ModerationActionSearchCriteria searchCriteria);
    }
}
