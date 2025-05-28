using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using AiLanguageApp.Models;
using System.Text.Json; // For potential JSON serialization later
using System.Collections.Generic; // For using List<T>

namespace AiLanguageApp.Pages
{
    
    public class GeneratorModel : PageModel
    {
            private static readonly Dictionary<char, SoundParameters> CharacterSoundMap = new Dictionary<char, SoundParameters>
        {
            // Basic Latin Alphabet (Uppercase) - Starting at A3 (220 Hz), semi-tone increments
        // Duration: 200ms, Waveform: "sine"
        { 'A', new SoundParameters { Frequency = 220, Duration = 200, Waveform = "sine" } }, // A3
        { 'B', new SoundParameters { Frequency = 233, Duration = 200, Waveform = "sine" } }, // A#3/Bb3
        { 'C', new SoundParameters { Frequency = 247, Duration = 200, Waveform = "sine" } }, // B3
        { 'D', new SoundParameters { Frequency = 262, Duration = 200, Waveform = "sine" } }, // C4
        { 'E', new SoundParameters { Frequency = 277, Duration = 200, Waveform = "sine" } }, // C#4/Db4
        { 'F', new SoundParameters { Frequency = 294, Duration = 200, Waveform = "sine" } }, // D4
        { 'G', new SoundParameters { Frequency = 311, Duration = 200, Waveform = "sine" } }, // D#4/Eb4
        { 'H', new SoundParameters { Frequency = 330, Duration = 200, Waveform = "sine" } }, // E4
        { 'I', new SoundParameters { Frequency = 349, Duration = 200, Waveform = "sine" } }, // F4
        { 'J', new SoundParameters { Frequency = 370, Duration = 200, Waveform = "sine" } }, // F#4/Gb4
        { 'K', new SoundParameters { Frequency = 392, Duration = 200, Waveform = "sine" } }, // G4
        { 'L', new SoundParameters { Frequency = 415, Duration = 200, Waveform = "sine" } }, // G#4/Ab4
        { 'M', new SoundParameters { Frequency = 440, Duration = 200, Waveform = "sine" } }, // A4
        { 'N', new SoundParameters { Frequency = 466, Duration = 200, Waveform = "sine" } }, // A#4/Bb4
        { 'O', new SoundParameters { Frequency = 494, Duration = 200, Waveform = "sine" } }, // B4
        { 'P', new SoundParameters { Frequency = 523, Duration = 200, Waveform = "sine" } }, // C5
        { 'Q', new SoundParameters { Frequency = 554, Duration = 200, Waveform = "sine" } }, // C#5/Db5
        { 'R', new SoundParameters { Frequency = 587, Duration = 200, Waveform = "sine" } }, // D5
        { 'S', new SoundParameters { Frequency = 622, Duration = 200, Waveform = "sine" } }, // D#5/Eb5
        { 'T', new SoundParameters { Frequency = 659, Duration = 200, Waveform = "sine" } }, // E5
        { 'U', new SoundParameters { Frequency = 698, Duration = 200, Waveform = "sine" } }, // F5
        { 'V', new SoundParameters { Frequency = 740, Duration = 200, Waveform = "sine" } }, // F#5/Gb5
        { 'W', new SoundParameters { Frequency = 784, Duration = 200, Waveform = "sine" } }, // G5
        { 'X', new SoundParameters { Frequency = 831, Duration = 200, Waveform = "sine" } }, // G#5/Ab5
        { 'Y', new SoundParameters { Frequency = 880, Duration = 200, Waveform = "sine" } }, // A5
        { 'Z', new SoundParameters { Frequency = 932, Duration = 200, Waveform = "sine" } }, // A#5/Bb5

        // Digits - Starting at C3 (approx 131 Hz), semi-tone increments
        // Duration: 150ms, Waveform: "square"
        { '0', new SoundParameters { Frequency = 131, Duration = 150, Waveform = "square" } }, // C3
        { '1', new SoundParameters { Frequency = 139, Duration = 150, Waveform = "square" } }, // C#3/Db3
        { '2', new SoundParameters { Frequency = 147, Duration = 150, Waveform = "square" } }, // D3
        { '3', new SoundParameters { Frequency = 156, Duration = 150, Waveform = "square" } }, // D#3/Eb3
        { '4', new SoundParameters { Frequency = 165, Duration = 150, Waveform = "square" } }, // E3
        { '5', new SoundParameters { Frequency = 175, Duration = 150, Waveform = "square" } }, // F3
        { '6', new SoundParameters { Frequency = 185, Duration = 150, Waveform = "square" } }, // F#3/Gb3
        { '7', new SoundParameters { Frequency = 196, Duration = 150, Waveform = "square" } }, // G3
        { '8', new SoundParameters { Frequency = 208, Duration = 150, Waveform = "square" } }, // G#3/Ab3
        { '9', new SoundParameters { Frequency = 220, Duration = 150, Waveform = "square" } }, // A3 (Same freq as 'A' but different duration/waveform)

        // Space Character (Silence)
        // Duration: 100ms. Frequency and Waveform are typically irrelevant for silence.
        { ' ', new SoundParameters { Frequency = 0, Duration = 100, Waveform = "sine" } },

        // Punctuation and Symbols - Using "triangle" waveform, shorter durations
        // Frequencies chosen to be somewhat distinct or in a higher range.
        { '.', new SoundParameters { Frequency = 1000, Duration = 100, Waveform = "triangle" } },
        { ',', new SoundParameters { Frequency = 1050, Duration = 100, Waveform = "triangle" } },
        { '?', new SoundParameters { Frequency = 1100, Duration = 120, Waveform = "triangle" } }, // Slightly longer for emphasis
        { '!', new SoundParameters { Frequency = 1150, Duration = 120, Waveform = "triangle" } }, // Slightly longer for emphasis
        { '%', new SoundParameters { Frequency = 1200, Duration = 100, Waveform = "sawtooth" } }, // Different waveform
        { '$', new SoundParameters { Frequency = 1250, Duration = 100, Waveform = "sawtooth" } },
        { '@', new SoundParameters { Frequency = 1300, Duration = 100, Waveform = "sawtooth" } },
        { '"', new SoundParameters { Frequency = 1350, Duration = 80, Waveform = "triangle" } }, // Shorter
        { '\'', new SoundParameters { Frequency = 1400, Duration = 80, Waveform = "triangle" } } // Shorter
        // Add more symbols as needed following a similar pattern

        };
        private readonly ILogger<GeneratorModel> _logger;

        // Property to bind the text input from the form
        [BindProperty]
        [Required(ErrorMessage = "Please enter text to generate audio.")]
        public string? GeneratorInputText { get; set; } // The name matches the 'name' attribute of your textarea

        // Property to hold the generated sound code to display on the page
        // SupportsGet = true allows this property to be populated and displayed
        // even on a GET request (e.g., after a POST-Redirect-GET pattern, or if set in OnGet)
        [BindProperty(SupportsGet = true)]
        public string? SoundCodeDisplay { get; set; }

        // You might also want a property for general messages to the user
        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SoundParametersJson { get; set; }

        public GeneratorModel(ILogger<GeneratorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //Example: Set an initial message when the page loads
            Message = "Enter text below and click 'Generate & Play Sound'.";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "There was an error with your submission.";
                return Page();
            }

            var soundParamsList = new List<SoundParameters>(); // Initialize an empty list to hold the sound parameters

        if (!string.IsNullOrEmpty(GeneratorInputText)) // Check if there's any input text
            {
                foreach (char character in GeneratorInputText) // Loop through each character in the input text
                {
                    char upperChar = char.ToUpperInvariant(character); // Convert character to uppercase for case-insensitive lookup

                    // Try to get the sound parameters for the uppercase character from our map
                    if (CharacterSoundMap.TryGetValue(upperChar, out SoundParameters? soundParam))
                    {
                        if (soundParam != null) // Ensure soundParam is not null (TryGetValue can output default if key not found, though for class types it's null)
                        {
                            soundParamsList.Add(soundParam); // If found, add it to our list
                        }
                    }
                    // If character is not found in the map (after converting to uppercase),
                    // it's ignored, as per our defined rule.
                }
            }

    if (soundParamsList.Any())
    {
        SoundParametersJson = JsonSerializer.Serialize(soundParamsList);
        // You can keep the existing SoundCodeDisplay logic for debugging or remove it
        // SoundCodeDisplay = $"Generated {soundParamsList.Count} sound units. JSON ready.";
        // Message = "Text converted to sound parameters. Ready for client-side playback.";
    }
    else
    {
        SoundParametersJson = "[]"; // Empty JSON array if no parameters
        // SoundCodeDisplay = "No sound parameters generated for the input text.";
        // Message = "Input text did not contain any mappable characters.";
    }

    return Page();
}
    }
}
