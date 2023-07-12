namespace ShowControlSystem.Common.File; 

/// <summary>
/// A deserializer for interpreting a .rd document with schema version v1.
/// This class should be used within ShowControlSystem.Common only.
/// </summary>
internal class RundownSchemaV1Deserializer : AbstractRundownDeserializer {
    internal override Rundown Deserialize(string json) {
        var product = new Rundown();

        return product;
    }
}