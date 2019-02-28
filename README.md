# UnityVibrations
Simple wrapper for handling haptic feedback

### Right now contains implementation for Android system only.
For other systems you should create new implementation of *IVibrationSys* interface and accordingly change the *Vibrations* class to choose it inside its constructor.
### Simple Vibes and Patterns
* For simple vibration you should create *Vibe* instance through *Vibrations.Buzz(durationInMilliseconds)*
* For amplitude control you should create the basic *Vibe* instance and call *.Amplitude(amplitudeForce)* where *amplitudeForce* is byte value therefore is within the range of 1-255. Overall call: *Vibrations.Buzz(durationInMilliseconds).Amplitude(amplitudeForce)*
  * Please ensure that the device is capable of amplitude control behaviour because there are hardware limitations to that feature. You can check if this feature is supported by calling *.HasAmplitudes()* function on *Vibrations* class instance.
* For vibes inside Patterns (series of vibrations), you can specify the delay between each of the sequential elements by calling *.Delay(durationInMilliseconds)* The delay owned by the *Vibe* object is the time to wait before starting the vibration effect. This function affects only the Pattern playback behaviour. Overall call: *Vibrations.Buzz(durationInMilliseconds).Amplitude(amplitudeForce).Delay(durationInMilliseconds)* or you can also use it without amplitude control: *Vibrations.Buzz(durationInMilliseconds).Delay(durationInMilliseconds)*.
* To construct *Pattern* you should call *Vibrations.Buzz(vibes)* where *vibes* are any number of *Vibe* instances. Don't forget about delays in them!
* By default *Pattern* played just once, but any *Pattern* can be looped at specified index by calling *.LoopAt(repeatIndex)*. The first element has *repeatIndex=0*. Don't forget to stop looped *Pattern* by calling *.Stop()* at *Vibrations* class instance.
* The created instances can be played by calling *.Vibrate(Vibe/Pattern)* on *Vibrations* class instance. AGAIN! Don't forget to stop looped *Patterns*.

Simple examples are provided inside *ExampleBehaviour* class.
