# Feature Branch: us6-navigation

## 🧾 User Story 6
**Als Spieler möchte ich jederzeit zum Hauptmenü zurückkehren können.**

---

## 🎯 Ziel dieses Branches
Einfache und intuitive Rückkehr zum Hauptmenü (StartPage) aus allen Views.

---

## ✅ Implementierte Features
- Rückkehr-Buttons in allen Views (BlackJack, SlotMachine, GameSelection)
- Navigationslogik über EventHandler / Commands
- Einheitliche Navigation per `NavigationRequested` im ViewModel

---

## 📁 Relevanter Code

### In den Views (z. B. SlotMachineView.xaml)
```xml
<Button Content="Hauptmenü"
        Command="{Binding ReturnToMainMenuCommand}" ... />
```

### ViewModel-Logik
```csharp
public ICommand ReturnToMainMenuCommand { get; }
ReturnToMainMenuCommand = new RelayCommand(_ => OnNavigationRequested("MainMenu"));

private void OnNavigationRequested(string target)
{
    NavigationRequested?.Invoke(this, new NavigationEventArgs(target));
}
```

### MainWindow.xaml.cs
```csharp
viewModel.NavigationRequested += (s, e) =>
{
    if (e.TargetView == "MainMenu")
        MainFrame.Navigate(new StartPage());
};
```

---

## 🧩 Zusammenfassung
Dieser Branch ermöglicht jederzeit den Rückweg ins Hauptmenü – stabil & benutzerfreundlich.

📌 **Verlinkte Views:** alle Spielansichten

🧪 **Testbar durch: Klick auf „Hauptmenü“ in einer beliebigen Ansicht**
