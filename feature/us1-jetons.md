# Feature Branch: us1-jetons

## 🧾 User Story 1
**Als Spieler möchte ich einen Betrag in Jetons eingeben, um diesen für Spiele zu verwenden.**

---

## 🎯 Ziel dieses Branches
Implementierung eines Startbildschirms, auf dem der Spieler seinen Startbetrag in Jetons eingeben kann. Dieser Wert wird dann gespeichert und für spätere Spiele (BlackJack, SlotMachine) als Balance verwendet.

---

## ✅ Implementierte Features
- `StartPage.xaml` mit Eingabefeld für Startbetrag
- `Session.Instance.CurrentPlayer` wird mit Anfangs-Balance erstellt
- Weiterleitung zur Spielauswahl nach Bestätigung

---

## 📁 Relevanter Code

### StartPage.xaml
```xml
<TextBox x:Name="JetonInput"
         Width="150"
         Margin="0,10,0,10"
         HorizontalAlignment="Center"
         VerticalAlignment="Center"
         FontSize="16"/>

<Button Content="Starten"
        Click="StartButton_Click"
        HorizontalAlignment="Center"
        Padding="10,5"
        FontSize="16"
        Background="#2ECC71"
        Foreground="White"/>
```

### StartPage.xaml.cs
```csharp
private void StartButton_Click(object sender, RoutedEventArgs e)
{
    if (int.TryParse(JetonInput.Text, out int startBalance) && startBalance > 0)
    {
        Session.Instance.StartNewSession("Spieler", startBalance);

        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.Navigate(new GameSelectionView());
    }
    else
    {
        MessageBox.Show("Bitte eine gültige Zahl eingeben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
```

### Session.cs
```csharp
public void StartNewSession(string playerName, int initialBalance)
{
    CurrentPlayer = new Player(playerName, initialBalance);
    CurrentGameType = null;
    CurrentGame = null;
    LastBet = 0;
    LastResult = null;
    NotifyObservers();
}
```

---

## 🧩 Zusammenfassung
Dieser Branch enthält die Startlogik für Jeton-Eingabe, initialisiert den Spieler und leitet korrekt zur Spielauswahl weiter.

📌 **Verlinkte Views:** `StartPage.xaml`, `GameSelectionView.xaml`

🧪 **Testbar durch Start der Anwendung und Eingabe eines Betrags**
