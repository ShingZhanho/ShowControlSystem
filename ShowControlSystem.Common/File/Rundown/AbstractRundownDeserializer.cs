namespace ShowControlSystem.Common.File; 

/// <summary>
/// An abstract deserializer for rundown documents (*.rd).
/// Deserializers designed for all versions of schemas should derive from this class.
/// </summary>
internal abstract class AbstractRundownDeserializer {
    /// <summary>
    /// Deserialize the provided json string to a Rundown instance.
    /// </summary>
    /// <param name="json">The json string to be deserialized.</param>
    /// <returns>The deserialized instance (if successful).</returns>
    internal abstract Rundown Deserialize(string json);
}