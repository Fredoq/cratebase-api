using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record AudioFileFormat
{
    private AudioFileFormat(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static AudioFileFormat Flac { get; } = FromCode("flac");

    public static AudioFileFormat Mp3 { get; } = FromCode("mp3");

    public static AudioFileFormat Ogg { get; } = FromCode("ogg");

    public static AudioFileFormat FromCode(string code)
    {
        return new AudioFileFormat(Guard.RequiredText(code, nameof(code), "audio_file_format.code_required").ToLowerInvariant());
    }
}
