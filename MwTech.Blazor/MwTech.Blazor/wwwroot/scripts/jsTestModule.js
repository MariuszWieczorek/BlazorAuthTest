
// Test modułów w Java Script
// Aby to był moduł musimy dodać słowo export przed nazwą funkcji
// Nie musimy dodać odwołania do pliku JavaScript w pliku index.html czy też w App.razor.

export function addNumberJSModule(number1, number2) {
    var sum = number1 + number2;
    alert(sum);
}