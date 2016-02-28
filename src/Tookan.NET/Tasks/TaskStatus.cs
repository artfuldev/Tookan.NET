namespace Tookan.NET.Tasks
{
    /// <summary>
    ///     The possible values of the Task Status as an enumeration.
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        ///     The task has been assigned to a agent.
        /// </summary>
        Assigned = 0,

        /// <summary>
        ///     The task has been started and the agent is on the way. This will appear as a light-blue pin on the map and as
        ///     a rectangle in the assigned category on the left
        /// </summary>
        Started = 1,

        /// <summary>
        ///     The task has been completed successfully and will appear as a green pin on the map and as a rectangle in the
        ///     completed category on the left
        /// </summary>
        Successful = 2,

        /// <summary>
        ///     The task has been completed unsuccessfully and will appear as red pin on the map and as a rectangle in the
        ///     completed category on the left
        /// </summary>
        Failed = 3,

        /// <summary>
        ///     The task is being performed and the agent has reached the destination. This will appear as a dark-blue pin on
        ///     the map and as a rectangle in the assigned category on the left.
        /// </summary>
        InProgress = 4,

        /// <summary>
        ///     The task has not been assigned to any agent and will appear as a grey pin on the map and as a rectangle in the
        ///     unassigned category on the left.
        /// </summary>
        Unassigned = 6,

        /// <summary>
        ///     The task has been accepted by the agent who was assigned the task.
        /// </summary>
        Accepted = 7,

        /// <summary>
        ///     The task has been declined by the agent who was assigned the task.
        /// </summary>
        Declined = 8,

        /// <summary>
        ///     The task has been cancelled by the agent who was assigned the task.
        /// </summary>
        Canceled = 9
    }
}