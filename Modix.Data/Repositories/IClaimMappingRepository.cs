﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Modix.Data.Models;
using Modix.Data.Models.Core;

namespace Modix.Data.Repositories
{
    /// <summary>
    /// Describes a repository for managing <see cref="ClaimMappingEntity"/> entities, within an underlying data storage provider.
    /// </summary>
    public interface IClaimMappingRepository
    {
        /// <summary>
        /// Begins a new transaction to create mappings within the repository.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that will complete, with the requested transaction object,
        /// when no other transactions are active upon the repository.
        /// </returns>
        Task<IRepositoryTransaction> BeginCreateTransactionAsync();

        /// <summary>
        /// Creates a new claim mapping within the repository.
        /// </summary>
        /// <param name="data">The data for the mapping to be created.</param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="data"/>.</exception>
        /// <returns>
        /// A <see cref="Task"/> which will complete when the operation is complete,
        /// containing the auto-generated <see cref="ClaimMappingEntity.Id"/> value assigned to the new mapping.
        /// </returns>
        Task<long> CreateAsync(ClaimMappingCreationData data);

        /// <summary>
        /// Retrieves information about a claim mapping, based on its ID.
        /// </summary>
        /// <param name="roleClaimId">The <see cref="ClaimMappingEntity.Id"/> value of the mapping to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task"/> which will complete when the operation is complete,
        /// containing the requested mapping, or null if no such mapping exists.
        /// </returns>
        Task<ClaimMappingSummary> ReadAsync(long roleClaimId);

        /// <summary>
        /// Checks whether any claims exist, for an arbitrary set of criteria.
        /// </summary>
        /// <param name="criteria">A set of criteria defining the mappings to be checked for.</param>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the operation has completed,
        /// containing a flag indicating whether any matching claim mappings were found.
        /// </returns>
        Task<bool> AnyAsync(ClaimMappingSearchCriteria criteria);

        /// <summary>
        /// Searches the repository for claim mapping id values, based on an arbitrary set of criteria.
        /// </summary>
        /// <param name="criteria">The criteria for selecting <see cref="ClaimMappingEntity.Id"/> values to be returned.</param>
        /// <returns>A <see cref="Task"/> which will complete when the matching values have been retrieved.</returns>
        Task<IReadOnlyCollection<long>> SearchIdsAsync(ClaimMappingSearchCriteria criteria);

        /// <summary>
        /// Searches the repository for claim mapping information, based on an arbitrary set of criteria.
        /// </summary>
        /// <param name="criteria">The criteria for selecting <see cref="ClaimMappingBrief"/> records to be returned.</param>
        /// <returns>A <see cref="Task"/> which will complete when the matching records have been retrieved.</returns>
        Task<IReadOnlyCollection<ClaimMappingBrief>> SearchBriefsAsync(ClaimMappingSearchCriteria criteria);

        /// <summary>
        /// Marks an existing claim mapping as rescinded, based on its ID.
        /// </summary>
        /// <param name="claimMappingId">The <see cref="ClaimMappingEntity.Id"/> value of the mapping to be rescinded.</param>
        /// <param name="rescindedById">The <see cref="UserEntity.Id"/> value of the user that is rescinding the mapping.</param>
        /// A <see cref="Task"/> which will complete when the operation is complete,
        /// containing a flag indicating whether the update was successful (I.E. whether the specified mapping could be found).
        /// </returns>
        Task<bool> TryDeleteAsync(long claimMappingId, ulong rescindedById);
    }
}
