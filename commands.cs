public static class ApplicationCommands
{
    public static class Definitions
    {
        public static IUICommand CreateSayHello() => new UICommand<string>(
            name: "SayHello",
            text: "Say Hello",
            toolTip: "Greets the user",
            hotKey: new KeyGesture(Key.H, ModifierKeys.Control));

        public static IUICommand CreateSave() => new UICommand<object>(
            name: "Save",
            text: "Save",
            toolTip: "Saves the current state",
            hotKey: new KeyGesture(Key.S, ModifierKeys.Control));
    }

    // Optional: Cache shared instances if you want to reuse them in specific cases
    public static readonly IUICommand SayHello = Definitions.CreateSayHello();
    public static readonly IUICommand Save = Definitions.CreateSave();
}

public class MainViewModel : IViewModel
{
    public IUICommand SayHello { get; }
    public IUICommand Save { get; }

    public MainViewModel()
    {
        // Create new instances for this view model
        SayHello = ApplicationCommands.Definitions.CreateSayHello();
        Save = ApplicationCommands.Definitions.CreateSave();

        // Bind commands to this view model's logic
        InitializeCommand(SayHello, this);
        InitializeCommand(Save, this);
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
            case "SayHello":
                MessageBox.Show("Hello from this view model!");
                break;
            case "Save":
                SaveData();
                break;
        }
    }

    private bool CanSave() => true;
    private void SaveData() { /* Save logic */ }
}

<Button Content="Say Hello" Command="{Binding SayHello.Command}" />
<Button Content="Save" Command="{Binding Save.Command}" />
