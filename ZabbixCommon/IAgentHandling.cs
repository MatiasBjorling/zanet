using System;

namespace ZabbixCommon
{
	/// <summary>
	/// Summary description for IAgentHandling.
	/// </summary>
	public interface IAgentHandling
	{
		/// <summary>
		/// Start's the agent
		/// </summary>
		/// <returns></returns>
		void Start();

		/// <summary>
		/// Stops the agent. The agent is handling the closing of threads it has created and only returns when everything is closed.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		void Stop();
	}
}
