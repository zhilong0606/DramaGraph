// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Assets/DramaGraph/NodeDefineIl/Structure.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace DramaScript {

  /// <summary>Holder for reflection information generated from Assets/DramaGraph/NodeDefineIl/Structure.proto</summary>
  public static partial class StructureReflection {

    #region Descriptor
    /// <summary>File descriptor for Assets/DramaGraph/NodeDefineIl/Structure.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static StructureReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ci5Bc3NldHMvRHJhbWFHcmFwaC9Ob2RlRGVmaW5lSWwvU3RydWN0dXJlLnBy",
            "b3RvEgtEcmFtYVNjcmlwdCIWChREcmFtYVNjcmlwdERhdGFFbnRyeSIVChNE",
            "cmFtYVNjcmlwdERhdGFFeGl0IigKGERyYW1hU2NyaXB0RGF0YVRpbWVFbnRy",
            "eRIMCgRUaW1lGAEgASgCIkQKHERyYW1hU2NyaXB0RGF0YVBsYXlBbmltYXRp",
            "b24SFQoNQW5pbWF0aW9uTmFtZRgBIAEoCRINCgVTcGVlZBgCIAEoAmIGcHJv",
            "dG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::DramaScript.DramaScriptDataEntry), global::DramaScript.DramaScriptDataEntry.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::DramaScript.DramaScriptDataExit), global::DramaScript.DramaScriptDataExit.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::DramaScript.DramaScriptDataTimeEntry), global::DramaScript.DramaScriptDataTimeEntry.Parser, new[]{ "Time" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::DramaScript.DramaScriptDataPlayAnimation), global::DramaScript.DramaScriptDataPlayAnimation.Parser, new[]{ "AnimationName", "Speed" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class DramaScriptDataEntry : pb::IMessage<DramaScriptDataEntry> {
    private static readonly pb::MessageParser<DramaScriptDataEntry> _parser = new pb::MessageParser<DramaScriptDataEntry>(() => new DramaScriptDataEntry());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DramaScriptDataEntry> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::DramaScript.StructureReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataEntry() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataEntry(DramaScriptDataEntry other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataEntry Clone() {
      return new DramaScriptDataEntry(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DramaScriptDataEntry);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DramaScriptDataEntry other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DramaScriptDataEntry other) {
      if (other == null) {
        return;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  public sealed partial class DramaScriptDataExit : pb::IMessage<DramaScriptDataExit> {
    private static readonly pb::MessageParser<DramaScriptDataExit> _parser = new pb::MessageParser<DramaScriptDataExit>(() => new DramaScriptDataExit());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DramaScriptDataExit> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::DramaScript.StructureReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataExit() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataExit(DramaScriptDataExit other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataExit Clone() {
      return new DramaScriptDataExit(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DramaScriptDataExit);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DramaScriptDataExit other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DramaScriptDataExit other) {
      if (other == null) {
        return;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  public sealed partial class DramaScriptDataTimeEntry : pb::IMessage<DramaScriptDataTimeEntry> {
    private static readonly pb::MessageParser<DramaScriptDataTimeEntry> _parser = new pb::MessageParser<DramaScriptDataTimeEntry>(() => new DramaScriptDataTimeEntry());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DramaScriptDataTimeEntry> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::DramaScript.StructureReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataTimeEntry() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataTimeEntry(DramaScriptDataTimeEntry other) : this() {
      time_ = other.time_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataTimeEntry Clone() {
      return new DramaScriptDataTimeEntry(this);
    }

    /// <summary>Field number for the "Time" field.</summary>
    public const int TimeFieldNumber = 1;
    private float time_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Time {
      get { return time_; }
      set {
        time_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DramaScriptDataTimeEntry);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DramaScriptDataTimeEntry other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Time != other.Time) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Time != 0F) hash ^= Time.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Time != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(Time);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Time != 0F) {
        size += 1 + 4;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DramaScriptDataTimeEntry other) {
      if (other == null) {
        return;
      }
      if (other.Time != 0F) {
        Time = other.Time;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 13: {
            Time = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  public sealed partial class DramaScriptDataPlayAnimation : pb::IMessage<DramaScriptDataPlayAnimation> {
    private static readonly pb::MessageParser<DramaScriptDataPlayAnimation> _parser = new pb::MessageParser<DramaScriptDataPlayAnimation>(() => new DramaScriptDataPlayAnimation());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DramaScriptDataPlayAnimation> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::DramaScript.StructureReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataPlayAnimation() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataPlayAnimation(DramaScriptDataPlayAnimation other) : this() {
      animationName_ = other.animationName_;
      speed_ = other.speed_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DramaScriptDataPlayAnimation Clone() {
      return new DramaScriptDataPlayAnimation(this);
    }

    /// <summary>Field number for the "AnimationName" field.</summary>
    public const int AnimationNameFieldNumber = 1;
    private string animationName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string AnimationName {
      get { return animationName_; }
      set {
        animationName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Speed" field.</summary>
    public const int SpeedFieldNumber = 2;
    private float speed_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Speed {
      get { return speed_; }
      set {
        speed_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DramaScriptDataPlayAnimation);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DramaScriptDataPlayAnimation other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AnimationName != other.AnimationName) return false;
      if (Speed != other.Speed) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (AnimationName.Length != 0) hash ^= AnimationName.GetHashCode();
      if (Speed != 0F) hash ^= Speed.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (AnimationName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(AnimationName);
      }
      if (Speed != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Speed);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (AnimationName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AnimationName);
      }
      if (Speed != 0F) {
        size += 1 + 4;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DramaScriptDataPlayAnimation other) {
      if (other == null) {
        return;
      }
      if (other.AnimationName.Length != 0) {
        AnimationName = other.AnimationName;
      }
      if (other.Speed != 0F) {
        Speed = other.Speed;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            AnimationName = input.ReadString();
            break;
          }
          case 21: {
            Speed = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
