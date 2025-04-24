# Feature Branch: us4-blackjack

## 🧾 User Story 4
**Als Spieler möchte ich bei BlackJack einen Einsatz setzen und gegen den Computer spielen.**

---

## 🎯 Ziel dieses Branches
BlackJack-Logik inklusive Computergegnern, Einsatzlogik und Punktbewertung korrekt umsetzen.

---

## ✅ Implementierte Features
- BlackJack Model mit Spieler- und Gegnerlogik
- Einsatz und Kartenvergabe mit Zufall
- Punktberechnung mit Ass = 11 oder 1
- Ergebnisanzeige und Spielentscheidungen

---

## 📁 Relevanter Code

### BlackJack.cs
```csharp
public bool StartGame(Player player, int bet, int numOpponents)
{
    // Karten austeilen an Spieler & Computer
    // Einsatz abziehen
}

public bool ExecuteAction(string actionName)
{
    // Hit / Stand / DoubleDown Logik
}
```

### BlackJackViewModel.cs
```csharp
public ICommand HitCommand { get; }
public ICommand StandCommand { get; }

private void UpdateGameState()
{
    PlayerCards = new ObservableCollection<Card>(_game.PlayerHand);
    ComputerPlayers = new ObservableCollection<ComputerPlayerViewModel>(...);
    Balance = _currentPlayer.Balance;
}
```

---

## 🧩 Zusammenfassung
Dieser Branch enthält das vollständige BlackJack-Spiel gegen Computergegner inkl. Punktelogik und Einsatzsystem.

📌 **Verlinkte Klassen:** `BlackJack.cs`, `BlackJackViewModel.cs`, `BlackJackView.xaml`

🧪 **Testbar durch: BlackJack-Spiel starten, Karten ziehen, Spiel beenden**
