function addNumberJS(number1, number2) {
    var sum = number1 + number2;
    alert(sum);
}


// poniżej w skryptach JC wywołujemy metody C#
// ważne w wywołaniach DotNet.invokeMethodAsync podać namespace projektu, w którym chcemy użyć tej funkcji

function addNumberCSharp(number1, number2) {
    DotNet.invokeMethodAsync("MwTech.Blazor.Client", "Add", parseInt(number1), parseInt(number2))
        .then(result => {
            alert(result);
        })
}

function GetCurrentDateCSharp() {
    return DotNet.invokeMethodAsync("MwTech.Blazor.Client", "GetCurrentDate");
}