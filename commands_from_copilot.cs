public static class UICommandBuilder
{
    public static UICommand<T> Create<T>(
        CommandMetadata meta,
        IViewModel context)
    {
        var command = new UICommand<T>(
            name: meta.Name,
            text: meta.Text,
            toolTip: meta.ToolTip,
            hotKey: meta.HotKey);

        InitializeCommand(command, context);
        return command;
    }
}


public record CommandMetadata(
    string Name,
    string Text,
    string ToolTip,
    KeyGesture HotKey);


public static class CommandTemplates
{
    public static readonly CommandMetadata SayHello =
        new("SayHello", "Hello", "Greets the user", new KeyGesture(Key.H, ModifierKeys.Control));

    public static readonly CommandMetadata Save =
        new("Save", "Save", "Saves data", new KeyGesture(Key.S, ModifierKeys.Control));
}


public class MainViewModel : IViewModel
{
    public UICommand<string> SayHello { get; }
    public UICommand<object> Save { get; }

    public MainViewModel()
    {
        SayHello = UICommandBuilder.Create<string>(CommandTemplates.SayHello, this);
        Save = UICommandBuilder.Create<object>(CommandTemplates.Save, this);
    }

    public bool CommandCanExecute(IUICommand command)
    {
        return command.Name switch
        {
            "SayHello" => true,
            "Save" => CanSave(),
            _ => false
        };
    }

    public void CommandExecuted(IUICommand command)
    {
        switch (command.Name)
        {
            case "SayHello": MessageBox.Show("Hi there!"); break;
            case "Save": SaveData(); break;
        }
    }

    private bool CanSave() => true;
    private void SaveData() { /* saving logic */ }
}
