using Hexus.Daemon.Contracts;
using Hexus.Daemon.Contracts.Requests;
using Hexus.Daemon.Contracts.Responses;
using System.Text.Json.Serialization;

namespace Hexus.Daemon;

[JsonSerializable(typeof(HttpValidationProblemDetails))]
[JsonSerializable(typeof(HexusApplicationResponse))]
[JsonSerializable(typeof(IEnumerable<HexusApplicationResponse>))]
[JsonSerializable(typeof(IAsyncEnumerable<ApplicationLog>))]
[JsonSerializable(typeof(NewApplicationRequest))]
[JsonSerializable(typeof(EditApplicationRequest))]
[JsonSerializable(typeof(SendInputRequest))]
[JsonSourceGenerationOptions(UseStringEnumConverter = true)]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
