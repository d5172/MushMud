using Agatha.Common;
using Agatha.Common.InversionOfControl;
using Agatha.ServiceLayer;
using MusicCompany.Common.Queries;
using MusicCompany.ServiceLayer.QueryHandlers;

namespace MusicCompany.ServiceLayer.Support
{
	/// <summary>
	/// Configures the service layer for apps where the client and servicelayer
	/// are running in the same process.
	/// </summary>
	public static class SingleProcessConfiguration
	{
		public static void Initialize()
		{
			IContainer agathaContainer = new SpringContainer();

			var serviceLayerConfig = new ServiceLayerConfiguration(
				  typeof(ListArtistsQueryHandler).Assembly,
					typeof(ListArtistsQuery).Assembly,
					agathaContainer
				);

			//serviceLayerConfig.RequestProcessorImplementation = typeof(CommandLoggingRequestProcessor);

			var clientConfig = new ClientConfiguration(typeof(ListArtistsQuery).Assembly,
					agathaContainer);

			serviceLayerConfig.Initialize();
			clientConfig.Initialize();
		}
	}
}