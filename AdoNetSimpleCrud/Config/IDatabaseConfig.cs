using System;
using System.Collections.Generic;
using System.Text;

namespace Ado.Net.Config
{
    /// <summary>
    /// Interface for configuring database before interaction.
    /// </summary>
    public interface IDatabaseConfig
    {
        /// <summary>
        /// Get Connection String.
        /// </summary>
        /// <param name="connectionStringName">Connection strings name.</param>
        /// <returns></returns>
        public string GetDbConnectionString(string connectionStringName);
    }
}
