# Feature Branch: us2-auswahl

## 🧾 User Story 2
**Als Spieler möchte ich zwischen Slotmachine und BlackJack wählen können.**

---

## 🎯 Ziel dieses Branches
Implementierung eines Auswahlbildschirms, auf dem der Spieler zwischen den verfügbaren Spielen wählen kann.

---

## ✅ Implementierte Features
- `GameSelectionView.xaml` mit zwei Spielkarten (SlotMachine & BlackJack)
- Befehle im ViewModel: `StartSlotMachineCommand`, `StartBlackJackCommand`
- Visuals und Beschreibungstexte je Spiel

---

## 📁 Relevanter Code

### GameSelectionView.xaml (Ausschnitt)
```xml
<TextBlock Text="Wähle dein Spiel:" ... />

<Button Content="Slot Machine spielen"
        Command="{Binding StartSlotMachineCommand}" ... />

<Button Content="BlackJack spielen"
        Command="{Binding StartBlackJackCommand}" ... />
```

### GameSelectionViewModel.cs
```csharp
public ICommand StartSlotMachineCommand { get; }
public ICommand StartBlackJackCommand { get; }

StartSlotMachineCommand = new RelayCommand(_ => NavigateTo("SlotMachineView"));
StartBlackJackCommand = new RelayCommand(_ => NavigateTo("BlackJackView"));

private void NavigateTo(string viewName)
{
    NavigationRequested?.Invoke(this, new NavigationEventArgs(viewName));
}
```

---

## 🧩 Zusammenfassung
Dieser Branch enthält die grafische und funktionale Umsetzung der Spielauswahl. Der Spieler wird entsprechend seiner Auswahl zur Spielansicht weitergeleitet.

📌 **Verlinkte Views:** `GameSelectionView.xaml`, `SlotMachineView`, `BlackJackView`

🧪 **Testbar durch: Auswahlfenster nach Start anzeigen und Spiel auswählen**
