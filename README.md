# AiLanguageApp
Building an Interactive Audio Language Generator: A Complete Guide to ASP.NET Core, Web Audio API, and Azure Deployment

## Project Overview
AiLanguageApp is an innovative web application that transforms text into a unique audio "language" using rule-based sound synthesis. Built with ASP.NET Core Razor Pages and powered by the Web Audio API, this application converts textual characters into distinct audio frequencies, durations, and waveforms, creating a playable audio representation of written text. The project demonstrates modern web development practices, client-side audio manipulation, and cloud deployment strategies.

## Features
- **Text-to-Audio Conversion:** Transform any text input into a sequence of audio tones
- **Custom Sound Mapping:** Rule-based character-to-sound encoding system with unique frequencies
- **Multi-Waveform Support:** Different waveforms (sine, square, triangle, sawtooth) for character categories
- **Real-time Audio Playback:** Instant audio generation and playback using Web Audio API
- **Comprehensive Character Set:** Support for letters (A-Z), digits (0-9), punctuation, and special characters
- **Responsive Web Interface:** Clean, modern UI built with Razor Pages and Bootstrap
- **Server-side Validation:** Robust input validation and error handling
- **Educational Tool:** Designed for learning full-stack development and audio programming

## Character Encoding System
The application uses a sophisticated mapping system that converts text characters into audio parameters:

- **Letters (A-Z):** Sine wave tones from 220Hz-932Hz (200ms duration)
- **Digits (0-9):** Square wave tones from 131Hz-220Hz (150ms duration)  
- **Punctuation:** Triangle/sawtooth waves at 1000Hz+ (80-120ms duration)
- **Spaces:** Silence periods (100ms duration)
- **Case Insensitive:** All input converted to uppercase for consistent mapping

## How It Was Built
- **Backend:** ASP.NET Core 9.0 Razor Pages with C#
- **Frontend:** Server-rendered Razor views with JavaScript for audio synthesis
- **Audio Engine:** Web Audio API with OscillatorNode and GainNode integration
- **Data Structure:** Custom SoundParameters class for frequency/duration/waveform mapping
- **Architecture:** MVC pattern with PageModel handlers and client-side audio processing
- **Deployment:** Azure App Service ready with F1 tier compatibility

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Visual Studio 2022 or Visual Studio Code with C# Dev Kit
- Modern web browser with Web Audio API support

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/AiLanguageApp.git
   cd AiLanguageApp
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Open your browser to `https://localhost:7xxx` (port varies)

### Usage
1. Navigate to the **Generator** page
2. Enter any text in the input field
3. Click **"Generate & Play Sound"**
4. Listen as your text is converted to audio tones
5. Visit the **About** page to learn more about the encoding system

## Technical Architecture

### Sound Parameter Structure
```csharp
public class SoundParameters
{
    public int Frequency { get; set; }    // Hz (0 for silence)
    public int Duration { get; set; }     // milliseconds
    public string? Waveform { get; set; } // "sine", "square", "triangle", "sawtooth"
}
```

### Audio Processing Flow
1. **Input Processing:** Text input validated and processed server-side
2. **Character Mapping:** Each character converted to SoundParameters via dictionary lookup
3. **JSON Serialization:** Sound data serialized for client-side consumption
4. **Audio Synthesis:** JavaScript creates AudioContext and OscillatorNode chains
5. **Playback Scheduling:** Precise timing using Web Audio API scheduling

## Development Phases
This project was built following a structured approach:

- ✅ **Phase 1:** Project foundation and environment setup
- ✅ **Phase 2:** User interface development with Razor Pages
- ✅ **Phase 3:** Core audio language generator implementation
- 🔄 **Phase 4:** Testing and quality assurance (in progress)
- 📋 **Phase 5:** Azure cloud deployment (planned)

## Deployment
The application is designed for easy deployment to Microsoft Azure App Service:

- Framework-dependent deployment model
- Compatible with Azure F1 (Free) tier
- Production-ready configuration included
- Supports both Windows and Linux hosting

---

Feel free to use this project as a learning resource for ASP.NET Core development, Web Audio API programming, or as a foundation for building more complex audio applications. For detailed implementation notes and development guidelines, see the included `spec.md` file.
