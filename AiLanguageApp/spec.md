# Project: AI Language Generator

**Goal:** To create a web application, the "AI Language Generator," using ASP.NET Core Razor Pages and Azure. The application will convert textual input into a custom rule-based audio "language" (based on specific sound properties like frequency and duration). This project serves as a pedagogical tool for learning full-stack web development, data encoding, client-side audio manipulation with the Web Audio API, and cloud deployment practices.

# Components

## Environment/Hosting (Recommended Initial Setup)
- **Local Development Machine (Windows/macOS/Linux)**
  - .NET SDK (Latest stable, e.g., .NET 8 or .NET 9)
  - IDE: Visual Studio 2022 (with "ASP.NET and web development" workload) / Visual Studio Code (with C# Dev Kit extension)
  - Git for version control
- **Cloud Hosting:** Microsoft Azure App Service (Free Tier F1 recommended for initial deployment)

## Software Components (Recommended Technologies)

### Web Application Backend & Frontend
- **Framework:** ASP.NET Core Razor Pages
- **Language:** C#
- **Frontend Rendering:** Razor Syntax, HTML5, CSS
- **Client-Side Scripting/Audio:** JavaScript, Web Audio API

### Data Storage/Representation (for "Language" Definition)
- **Encoding Scheme Storage:** C# `Dictionary<char, SoundParameters>` (initially hardcoded)
- **"Language" Data Transfer:** JSON strings representing sequences of sound parameters for generation.

### Version Control
- **System:** Git

---

# "AI Language" Definition & Data Structures

## `SoundParameters` Object/Struct (C# & JavaScript representation)
- `Frequency` (int, Required, Hertz - Hz)
- `Duration` (int, Required, milliseconds - ms)
- `Waveform` (string, Optional but recommended, e.g., "sine", "square", "sawtooth", "triangle" - values supported by Web Audio API `OscillatorNode.type`)

## Character-to-Sound Encoding Scheme
- **Implementation:** C# `Dictionary<char, SoundParameters>`
- **Scope:** Maps textual characters to unique `SoundParameters`.
- **Initial Character Set Example:** Uppercase letters (A-Z), digits (0-9), space character.
- **Handling Considerations:**
    - Case sensitivity (e.g., 'a' vs 'A').
    - Punctuation (e.g., '.', ',', '!').
    - Unknown characters (ignore, default sound, or error).
- **Example Entry:** `'A' -> { Frequency = 440, Duration = 200, Waveform = "sine" }`
- **Special Characters Example:**
    - `' ' (space) -> { Frequency = 0 (silence), Duration = 100, Waveform = "N/A" }`
    - `'.' (period) -> { Frequency = 300 (example), Duration = 50, Waveform = "square" }`

---

# Development Plan (MVP - Recommended Approach)

## Phase 1: Project Foundation and Setup
- [x] Define the scope and approach for the "AI Language" (rule-based, character-to-sound parameters).
- [x] Select ASP.NET Core Razor Pages as the web framework.
- [ ] Configure the development environment:
    - [x] Install .NET SDK (latest stable).
    - [x] Install IDE (Visual Studio 2022 or VS Code with C# Dev Kit).
    - [x] Install Git.
- [x] Create a Microsoft Azure account (Free Tier sufficient).
- [x] Initialize a local Git repository for the project.
- [x] Document key technology choices and rationale (Web Framework, Backend Language, Client-Side Audio, Hosting Platform, Version Control, IDE).

## Phase 2: Building the User Interface (Simple & Functional)
- [x] Sketch basic UI/UX flow for the Generator section.
- [x] Implement core UI elements using Razor Pages (.cshtml):
    - [x] Create `Generator.cshtml` with:
        - [x] Input `<textarea>` for generator text.
        - [x] Button to "Generate & Play Sound".
        - [x] Optional: Display area for intermediate "sound code".
    - [x] Create `About.cshtml` to describe the application.
    - [x] Update `Index.cshtml` to be the main landing/home page.
    - [x] Update `_Layout.cshtml` to include navigation links to Home, Generator, and About pages.
- [x] Implement PageModel logic (.cshtml.cs) for UI interactions:
    - [x] Define `[BindProperty]` properties for inputs and outputs.
    - [x] Implement `OnGet()` for initial page load.
    - [x] Implement `OnPost...Async()` named handler methods for form submissions.
- [x] Utilize a shared `_Layout.cshtml` for common page structure.
- [x] Implement basic server-side validation using data annotations and `ModelState.IsValid`.
- [x] Create a custom JavaScript file (e.g., `site.js`) and reference it for client-side enhancements.
- [x] Identify Web Audio API as the client-side technology for sound synthesis.

## Phase 3: Implementing the AI Language Generator
- [x] Design the Character-to-Sound Encoding Scheme:
    - [x] Define the C# `SoundParameters` class/struct (Frequency, Duration, Waveform (Sine)).
    - [x] Implement the mapping (e.g., `Dictionary<char, SoundParameters>`), initially hardcoded in C#.
    - [x] Define initial character set (e.g., A-Z, 0-9, space, . , ? ! % $ @ " ')
    - [x] Define rules for unmapped characters (e.g., ignore, default sound, error) and case sensitivity.
        - Rule: Convert input characters to uppercase before map lookup.
        - Rule: Ignore characters not found in the map after uppercase conversion.
- [x] Implement Server-Side Text-to-Sound Parameter Conversion:
    - [x] C# logic in PageModel (or a service) to convert input text into a list/array of `SoundParameters` objects.
- [x] Implement Client-Side Audio Synthesis (JavaScript & Web Audio API):
    - [x] JavaScript function to receive the list of `SoundParameters`.
    - [x] Initialize `AudioContext`.
    - [x] For each `SoundParameters` object:
        - [x] Create and configure `OscillatorNode` (type, frequency).
        - [x] Create and configure `GainNode` (for volume control and smooth start/stop).
        - [x] Connect audio graph: `OscillatorNode` -> `GainNode` -> `audioContext.destination`.
        - [x] Schedule tone playback precisely using `oscillator.start(startTime)` and `oscillator.stop(stopTime)` based on `audioContext.currentTime`.
- [x] Integrate Generator with the UI:
    - [x] Wire "Generate & Play Sound" button to a JavaScript function.
    - [x] Transfer `SoundParameters` list from server to client (e.g., AJAX call returning JSON, or embedding in page on post-back).
    - [x] Provide UI feedback (e.g., "Playing sound...").

## Phase 4: Ensuring Quality: Testing and Best Practices
- [ ] **Unit Testing (using xUnit or MSTest):** (See Testing Checklist for details)
    - [ ] Create a separate test project.
    - [ ] Test Character-to-Sound Parameter mapping.
    - [ ] Test Text-to-List-of-Sound-Parameters conversion.
    - [ ] Cover happy paths, edge cases, and error conditions.
- [ ] **Integration Testing (using `Microsoft.AspNetCore.Mvc.Testing`):** (See Testing Checklist for details)
    - [ ] Test Generator form submission and PageModel processing.
    - [ ] Test server-side validation logic.
- [ ] **Adhere to ASP.NET Core Best Practices:**
    - [ ] Use `async` and `await` for I/O-bound operations in PageModel handlers.
    - [ ] Apply Dependency Injection (DI) if custom services are created.
    - [ ] Use ASP.NET Core's configuration system for any externalized settings.
    - [ ] Implement robust error handling (try-catch, user-friendly messages).
    - [ ] Integrate logging using `ILogger`.
    - [ ] Maintain clear code organization (e.g., Single Responsibility Principle).
    - [ ] Ensure security considerations (e.g., anti-forgery tokens are utilized by default).

## Phase 5: Deployment to the Cloud: Azure Hosting
- [ ] Prepare the application for Azure deployment:
    - [ ] Verify NuGet package restoration.
    - [ ] Configure `appsettings.Production.json` if necessary (or use Azure App Settings).
    - [ ] Build the application in Release configuration.
    - [ ] Choose deployment mode (Framework-Dependent Deployment recommended for App Service with runtime).
- [ ] Deploy to Azure App Service:
    - [ ] Create an Azure App Service instance (Web App):
        - [ ] Specify Subscription, Resource Group, App name.
        - [ ] Configure Runtime stack (.NET version), Operating System, Region.
        - [ ] Create or select an App Service Plan (F1 Free tier suitable).
    - [ ] Deploy using chosen method (Visual Studio Publish wizard, Azure CLI, or set up CI/CD with GitHub Actions/Azure DevOps).
- [ ] Perform basic post-deployment verification and monitoring:
    - [ ] Browse to the application's public URL (e.g., `https://<appname>.azurewebsites.net`).
    - [ ] Test core generator functionalities.
    - [ ] Check Azure Log Stream for any runtime errors.
    - [ ] Review basic metrics in Azure portal (Overview blade).

---

# Testing Checklist (MVP)

- [ ] Project builds and runs locally without errors.
- **Unit Tests (C# Backend - e.g., xUnit/MSTest):**
    - [ ] `Character-to-SoundParameters` mapping: Correct `SoundParameters` returned for given characters.
    - [ ] Text to `List<SoundParameters>` conversion:
        - [ ] Handles valid input text correctly.
        - [ ] Handles empty strings.
        - [ ] Handles strings with characters not in the encoding scheme (as per defined rules).
- **Integration Tests (ASP.NET Core - `WebApplicationFactory`):**
    - [ ] **Generator Page:**
        - [ ] Submitting text via form POST to PageModel handler processes input.
        - [ ] PageModel makes sound parameters available (e.g., in model state or JSON response if AJAX).
    - [ ] **Form Validation:**
        - [ ] Server-side validation (`ModelState.IsValid`) works for required fields.
        - [ ] Appropriate error messages displayed for invalid input.
- **Manual Client-Side Testing (Browser):**
    - [ ] **Audio Playback:**
        - [ ] Correct sequence of tones plays for generated text.
        - [ ] Frequencies and durations of tones match the encoding scheme.
        - [ ] Waveforms (if implemented and varied) are distinguishable.
        - [ ] Playback is smooth without noticeable glitches (for reasonable length inputs).
    - [ ] **UI Feedback:**
        - [ ] Visual cues indicate when sound is being generated or played.
        - [ ] Buttons are appropriately enabled/disabled during operations if applicable.
- **Deployment Verification (Azure):**
    - [ ] Application loads and is accessible at the Azure App Service URL.
    - [ ] All core functionalities (Generate, Play) work as expected in the Azure environment.
    - [ ] No console errors in browser developer tools related to client-side script execution.
    - [ ] Azure Log Stream shows application starting and processing requests without critical errors.

---

# General Notes
- This project is designed as a pedagogical guide. The emphasis is on understanding both *what* to do and *why* specific technological and architectural choices are made.
- The "AI" in "AI Language Generator" refers to a deterministic, rule-based system, not machine learning or natural language processing.
- An iterative development approach is recommended: start with core functionality and enhance incrementally.
- Consistent use of Git for version control from the beginning is crucial.
- Focus on adhering to ASP.NET Core best practices throughout the project.
- Upon MVP completion, refer to the "Potential Enhancements and Further Learning Paths" section of the full guide for ideas on extending the project.