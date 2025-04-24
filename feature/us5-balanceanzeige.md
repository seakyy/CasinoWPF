# Feature Branch: us5-balanceanzeige

## 🧾 User Story 5
**Als Spieler möchte ich nach jeder Runde sehen, wie viele Jetons ich noch habe.**

---

## 🎯 Ziel dieses Branches
Die Anzeige des aktuellen Guthabens (Balance) soll immer aktuell sichtbar sein – während und nach dem Spiel.

---

## ✅ Implementierte Features
- `Balance` Property mit OnPropertyChanged in ViewModels
- Anzeige oben rechts in Spiel-Views (z. B. SlotMachineView & BlackJackView)
- Automatische Aktualisierung nach jedem Spielzug oder Einsatz

---

## 📁 Relevanter Code

### SlotMachineViewModel.cs / BlackJackViewModel.cs
```csharp
public int Balance {
    get => _balance;
    set {
        if (_balance != value) {
            _balance = value;
            OnPropertyChanged();
        }
    }
}
```

### View XAML (z. B. Header in SlotMachineView.xaml)
```xml
<TextBlock Text="{Binding Balance, StringFormat='Guthaben: {0} Jetons'}" ... />
```

### Nach Game-Ende
```csharp
Balance = Session.Instance.CurrentPlayer.Balance;
```

---

## 🧩 Zusammenfassung
Dieser Branch stellt sicher, dass die Jeton-Anzeige nach jeder Runde automatisch aktualisiert wird.

📌 **Verlinkt in allen ViewModels:** `Balance`-Property aktiv eingebunden

🧪 **Testbar durch: SPIN oder BlackJack spielen und auf Jeton-Anzeige achten**
