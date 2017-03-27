using System.Threading.Tasks;

namespace EnergonSoftware.BackpackPlanner.Core.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PermissionHelper
    {
        /// <summary>
        /// Checks for the read permission.
        /// </summary>
        /// <param name="state">The system state.</param>
        public static async Task CheckReadPermission(BackpackPlannerState state)
        {
            if(!await state.PlatformPermissionRequestFactory.Create(PermissionRequest.PermissionType.ReadStorage).Request(state).ConfigureAwait(false)) {
                throw new PermissionDeniedException(PermissionRequest.PermissionType.ReadStorage, "Read permission denied!");
            }
        }

        /// <summary>
        /// Checks for the write permission.
        /// </summary>
        /// <param name="state">The system state.</param>
        public static async Task CheckWritePermission(BackpackPlannerState state)
        {
            if(!await state.PlatformPermissionRequestFactory.Create(PermissionRequest.PermissionType.WriteStorage).Request(state).ConfigureAwait(false)) {
                throw new PermissionDeniedException(PermissionRequest.PermissionType.WriteStorage, "Write permission denied!");
            }
        }
    }
}
