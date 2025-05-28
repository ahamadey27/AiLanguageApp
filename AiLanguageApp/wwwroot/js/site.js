// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

console.log("site.js loaded successfully!");

// Function to play a sequence of sounds based on the provided parameters
function playSoundSequence(soundParamsArray) {
    if (!soundParamsArray || soundParamsArray.length === 0) {
        console.log("No sound parameters to play.");
        return;
    }

    // Create an AudioContext instance. It's best to create it once per page load,
    // or in response to a user gesture if browsers restrict auto-play.
    const audioContext = new (window.AudioContext || window.webkitAudioContext)();

    // Check if AudioContext was created successfully
    if (!audioContext) {
        console.error("Web Audio API is not supported in this browser.");
        alert("Web Audio API is not supported in this browser. Cannot play audio.");
        return;
    }
    
    // Resume AudioContext if it's in a suspended state (common in modern browsers until a user gesture)
    if (audioContext.state === 'suspended') {
        audioContext.resume();
    }

    let currentTime = audioContext.currentTime; // Use the AudioContext's current time for precise scheduling

    soundParamsArray.forEach(params => {
        if (params.Frequency === 0 && params.Duration > 0) { // Handle silence
            currentTime += params.Duration / 1000; // Add silence duration to current time
            return; // Skip oscillator creation for silence
        }
        
        if (params.Duration <= 0) { // Skip if duration is zero or negative
            return;
        }

        const oscillator = audioContext.createOscillator();
        const gainNode = audioContext.createGain();

        // Configure Oscillator
        oscillator.type = params.Waveform ? params.Waveform.toLowerCase() : 'sine'; // 'sine', 'square', 'sawtooth', 'triangle'
        oscillator.frequency.setValueAtTime(params.Frequency, currentTime);

        // Configure GainNode for smooth start/stop to avoid clicks
        // Start with 0 volume, ramp up quickly, then ramp down before stop
        gainNode.gain.setValueAtTime(0, currentTime);
        gainNode.gain.linearRampToValueAtTime(0.7, currentTime + 0.01); // Ramp up to 70% volume in 0.01s
        gainNode.gain.linearRampToValueAtTime(0, currentTime + ((params.Duration / 2) / 1000) - 0.01); // Ramp down just before stopping

        // Connect nodes: Oscillator -> GainNode -> Destination (speakers)
        oscillator.connect(gainNode);
        gainNode.connect(audioContext.destination);

        // Start and stop the oscillator
        oscillator.start(currentTime);
        oscillator.stop(currentTime + (params.Duration / 2) / 1000); // Convert duration from ms to seconds

        // Advance current time for the next sound
        currentTime += (params.Duration / 2) / 1000;
    });

    console.log("Sound sequence playback initiated. Total duration approx:", currentTime - audioContext.currentTime, "seconds");
}
