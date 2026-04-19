using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record AudioFileFormat
{
    public required string Code { get; init; }

    public static AudioFileFormat Flac => FromCode("flac");

    public static AudioFileFormat Mp3 => FromCode("mp3");

    public static AudioFileFormat Vorbis => FromCode("ogg");

    public static AudioFileFormat FromCode(string code)
    {
        return new AudioFileFormat
        {
            Code = Guard.RequiredText(code, nameof(code), "audio_file_format.code_required").ToLowerInvariant()
        };
    }
}
