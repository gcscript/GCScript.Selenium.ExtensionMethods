namespace GCScript.Selenium.ExtensionMethods.Enums;

/// <summary>
/// Enum representing various DOM events.
/// </summary>
public enum EDomEvent
{
    // Mouse Events
    Click,
    DblClick,
    MouseDown,
    MouseMove,
    MouseOut,
    MouseOver,
    MouseUp,
    MouseEnter,
    MouseLeave,
    ContextMenu,

    // Keyboard Events
    KeyDown,
    KeyPress,
    KeyUp,
    Input,

    // Focus Events
    Focus,
    Blur,
    FocusIn,
    FocusOut,

    // Form Events
    Submit,
    Reset,
    Change,

    // Screen Events
    Resize,
    Scroll,

    // Document Events
    DOMContentLoaded,
    ReadyStateChange,

    // Drag & Drop Events
    Drag,
    DragStart,
    DragEnd,
    DragEnter,
    DragLeave,
    DragOver,
    Drop,

    // Clipboard Events
    Copy,
    Cut,
    Paste,

    // Media Events
    Play,
    Pause,
    Ended,
    VolumeChange,
    TimeUpdate,

    // Other Events
    Load,
    Unload,
    Error,
    Abort,
    BeforeUnload,
    HashChange,
    PopState,
    Wheel,
    TouchStart,
    TouchMove,
    TouchEnd,
    TouchCancel
}
