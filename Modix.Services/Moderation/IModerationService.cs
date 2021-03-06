﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;

using Modix.Data.Models;
using Modix.Data.Models.Moderation;

namespace Modix.Services.Moderation
{
    /// <summary>
    /// Describes a service for performing moderation actions, within the application, within the context of a single incoming request.
    /// </summary>
    public interface IModerationService
    {
        /// <summary>
        /// Automatically configures role and channel permissions, related to moderation, for a given guild.
        /// </summary>
        /// <param name="guild">The guild to be configured.</param>
        /// <returns>A <see cref="Task"/> which will complete when the operation has complete.</returns>
        Task AutoConfigureGuldAsync(IGuild guild);

        /// <summary>
        /// Automatically configures role and channel permissions, related to moderation, for a given channel.
        /// </summary>
        /// <param name="channel">The channel to be configured.</param>
        /// <returns>A <see cref="Task"/> which will complete when the operation has complete.</returns>
        Task AutoConfigureChannelAsync(IChannel channel);

        /// <summary>
        /// Automatically rescinds any infractions that have expired.,
        /// based on <see cref="InfractionEntity.Duration"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> that will complete when the operation has completed.</returns>
        Task AutoRescindExpiredInfractions();

        /// <summary>
        /// Removes all moderation configuration settings for a guild, by deleting all of its <see cref="ModerationMuteRoleMappingEntity"/> entries.
        /// </summary>
        /// <param name="guild">The guild to be un-configured.</param>
        /// <returns>A <see cref="Task"/> which will complete when the operation has complete.</returns>
        Task UnConfigureGuildAsync(IGuild guild);

        /// <summary>
        /// Retrieves the currently-configured mute role (the role that is assigned to users to mute them) for a given guild.
        /// </summary>
        /// <param name="guild">The guild whose mute role is to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the operation has completed,
        /// containing the mute role currently configured for use within <paramref name="guild"/>.
        /// </returns>
        Task<IRole> GetMuteRoleAsync(IGuild guild);

        /// <summary>
        /// Sets the currently-configured mute role (the role that is assigned to users to mute them) for a given guild.
        /// </summary>
        /// <param name="guild">The guild whose mute role is to be set.</param>
        /// <param name="muteRole">The mute role to be used for <paramref name="guild"/>.</param>
        /// <returns>A <see cref="Task"/> that will complete when the operation has completed.</returns>
        Task SetMuteRoleAsync(IGuild guild, IRole muteRole);

        /// <summary>
        /// Retrieves the list of the Discord snowflake ID values of all the channels currently configured
        /// to receive logging messages from the moderation feature, for a given guild.
        /// </summary>
        /// <param name="guildId">The Discord snowflake ID value of the guild whose logging channel ID values are to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the operation has completed,
        /// containing the requested list of channel ID values.
        /// </returns>
        Task<IReadOnlyCollection<ulong>> GetLogChannelIdsAsync(ulong guildId);

        /// <summary>
        /// Retrieves the list of all channels currently configured to receive logging messages from the moderation feature,
        /// for a given guild.
        /// </summary>
        /// <param name="guild">The guild whose logging channels are to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the operation has completed,
        /// containing the requested list of channels.
        /// </returns>
        Task<IReadOnlyCollection<IMessageChannel>> GetLogChannelsAsync(IGuild guild);

        /// <summary>
        /// Configures a channel to receive logging messages from the moderation feature, for a given guild.
        /// </summary>
        /// <param name="guild">The guild whose logging messages are to be sent to <paramref name="logChannel"/>.</param>
        /// <param name="logChannel">The channel to received logging messages from <paramref name="guild"/>.</param>
        /// <returns>A <see cref="Task"/> that will complete when the operation has completed.</returns>
        Task AddLogChannelAsync(IGuild guild, IMessageChannel logChannel);

        /// <summary>
        /// Configures a channel to stop receiving logging messages from the moderation feature, for a given guild.
        /// </summary>
        /// <param name="guild">The guild whose logging messages are being sent to <paramref name="logChannel"/>.</param>
        /// <param name="logChannel">The channel that should no longer receive logging messages from <paramref name="guild"/>.</param>
        /// <returns>A <see cref="Task"/> that will complete when the operation has completed.</returns>
        Task RemoveLogChannelAsync(IGuild guild, IMessageChannel logChannel);

        /// <summary>
        /// Creates an infraction upon a specified user, and logs an associated moderation action.
        /// </summary>
        /// <param name="type">The value to user for <see cref="InfractionEntity.Type"/>.<</param>
        /// <param name="subjectId">The value to use for <see cref="InfractionEntity.SubjectId"/>.</param>
        /// <param name="reason">The value to use for <see cref="ModerationActionEntity.Reason"/></param>
        /// <param name="duration">The value to use for <see cref="InfractionEntity.Duration"/>.</param>
        /// <returns>A <see cref="Task"/> which will complete when the operation has completed.</returns>
        Task CreateInfractionAsync(InfractionType type, ulong subjectId, string reason, TimeSpan? duration);

        /// <summary>
        /// Marks an existing, active, infraction of a given type, upon a given user, as rescinded.
        /// </summary>
        /// <param name="type">The <see cref="InfractionEntity.Type"/> value of the infraction to be rescinded.</param>
        /// <param name="subjectId">The <see cref="InfractionEntity.SubjectId"/> value of the infraction to be rescinded.</param>
        /// <returns>A <see cref="Task"/> which will complete when the operation has completed.</returns>
        Task RescindInfractionAsync(InfractionType type, ulong subjectId);

        /// <summary>
        /// Marks an existing infraction as rescinded, based on its ID.
        /// </summary>
        /// <param name="infractionId">The <see cref="InfractionEntity.Id"/> value of the infraction to be rescinded.</param>
        /// <returns>A <see cref="Task"/> which will complete when the operation has completed.</returns>
        Task RescindInfractionAsync(long infractionId);

        /// <summary>
        /// Marks an existing infraction as deleted, based on its ID.
        /// </summary>
        /// <param name="infractionId">The <see cref="InfractionEntity.Id"/> value of the infraction to be deleted.</param>
        /// <returns>A <see cref="Task"/> which will complete when the operation has completed.</returns>
        Task DeleteInfractionAsync(long infractionId);

        /// <summary>
        /// Retrieves a collection of infractions, based on a given set of criteria.
        /// </summary>
        /// <param name="searchCriteria">The criteria defining which infractions are to be returned.</param>
        /// <param name="sortingCriterias">The criteria defining how to sort the infractions to be returned.</param>
        /// <returns>
        /// A <see cref="Task"/> which will complete when the operation has completed,
        /// containing the requested set of infractions.
        /// </returns>
        Task<IReadOnlyCollection<InfractionSummary>> SearchInfractionsAsync(InfractionSearchCriteria searchCriteria, IEnumerable<SortingCriteria> sortingCriterias = null);

        /// <summary>
        /// Retrieves a collection of infractions, based on a given set of criteria, and returns a paged subset of the results, based on a given set of paging criteria.
        /// </summary>
        /// <param name="searchCriteria">The criteria defining which infractions are to be returned.</param>
        /// <param name="sortingCriterias">The criteria defining how to sort the infractions to be returned.</param>
        /// <returns>A <see cref="Task"/> which will complete when the operation has completed, containing the requested set of infractions.</returns>
        Task<RecordsPage<InfractionSummary>> SearchInfractionsAsync(InfractionSearchCriteria searchCriteria, IEnumerable<SortingCriteria> sortingCriteria, PagingCriteria pagingCriteria);

        /// <summary>
        /// Retrieves a moderation action, based on its ID.
        /// </summary>
        /// <param name="moderationActionId">The <see cref="ModerationActionEntity.Id"/> value of the moderation action to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the operation has completed,
        /// containing the requested moderation action.
        /// </returns>
        Task<ModerationActionSummary> GetModerationActionSummaryAsync(long moderationActionId);

        /// <summary>
        /// Retrieves a collection of moderation actions, based on a given set of criteria.
        /// </summary>
        /// <param name="searchCriteria">The criteria defining which moderation actions are to be returned.</param>
        /// <param name="sortingCriterias">The criteria defining how to sort the moderation actions to be returned.</param>
        /// <returns>
        /// A <see cref="Task"/> which will complete when the operation has completed,
        /// containing the requested set of moderation actions.
        /// </returns>
        Task<IReadOnlyCollection<ModerationActionSummary>> SearchModerationActionsAsync(ModerationActionSearchCriteria searchCriteria);

        /// <summary>
        /// Retrieves a timestamp indicating the next time an existing infraction will be expiring.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the operation is complete,
        /// containing the requested timestamp value.
        /// </returns>
        Task<DateTimeOffset?> GetNextInfractionExpiration();
    }
}
