# Feature Branch: us3-slotrunde

## 🧾 User Story 3
**Als Spieler möchte ich bei Slotmachine eine Runde mit fixem Einsatz spielen und das Ergebnis sehen.**

---

## 🎯 Ziel dieses Branches
Funktionsfähiges SlotMachine-Spiel mit festen Einsätzen, Gewinnanzeige und Symboldarstellung.

---

## ✅ Implementierte Features
- Spielmechanik über `SlotMachine.cs` (Model)
- Anzeige in `SlotMachineView.xaml`
- Reel-Symbole mit Bildern & Zufallslogik
- Ergebnisanzeige und Gewinnberechnung

---

## 📁 Relevanter Code

### SlotMachine.cs (Auszug)
```csharp
public bool ExecuteAction(string actionName, ...)
{
    if (actionName == "spin")
    {
        SpinReels();
        return EndGame() != null;
    }
}
```

### SlotMachineViewModel.cs
```csharp
public ICommand SpinCommand { get; }

private async void Spin() {
    // Animation: Zufällige Bilder 500ms anzeigen
    // Dann echtes Ergebnis anzeigen
    _game.ExecuteAction("spin");
    Reel1DisplayImage = Reel1Image;
    ...
}
```

### SlotMachineView.xaml
```xml
<Image Source="{Binding Reel1DisplayImage}" ... />
<Image Source="{Binding Reel2DisplayImage}" ... />
<Image Source="{Binding Reel3DisplayImage}" ... />
```

---

## 🧩 Zusammenfassung
Dieser Branch enthält das komplette SlotMachine-Spiel inkl. Spin-Logik, Bildanzeige und Ergebnisauswertung.

📌 **Verlinkte Views:** `SlotMachineView.xaml`, `SlotMachineViewModel.cs`, `SlotMachine.cs`

🧪 **Testbar durch: Klick auf „SPIN!“ Button im SlotMachineView**
