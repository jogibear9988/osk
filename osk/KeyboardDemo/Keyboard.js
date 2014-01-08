if (!window.Keyboard)
    window.Keyboard = {};


Keyboard.Handler = function() {
}

Keyboard.CreateDelegate = function(instance, method) {
    return function() {
        return method.apply(instance, arguments);
    }
}
Keyboard.Handler.prototype =
{
    visibleToolbar: false,
    handleEachClick: true,
    inputControl: null,
    silverlightCtrl: null,

    Select: function(sender) {
        this.inputControl = sender;
    },

    SetText: function(isOk, newText) {
        if (this.handleEachClick || !this.inputControl || !newText || !isOk) return;
        this.inputControl.value = newText;
    },

    GetCurrentText: function() {
        if (this.inputControl)
            return this.inputControl.value;
        return '';
    },

    HandleTxtClick: function(sender, args) {
        if (!this.handleEachClick || args == null || args.PressedKey == null || this.inputControl == null) return;
        alert("You clicked: " + args.PressedKey);
        // simplified version of keyboard handling
        this.inputControl.value += args.PressedKey;
    }
}

var keyboard = null;
if (!keyboard) {
    keyboard = new Keyboard.Handler();
}

function HandleTxtClick(sender, args) {    
    alert("You clicked: " + args.PressedKey);    
}
function PluginLoaded(sender, args) {
    var slCtl = document.getElementById("Xaml1");
    slCtl.Content.silverKeyboard.addEventListener("KeyPressed", Keyboard.CreateDelegate(keyboard, keyboard.HandleTxtClick));
    slCtl.Content.silverKeyboard.VisibleHelperControls = keyboard.visibleToolbar;
    slCtl.Content.silverKeyboard.KeyboardHandler = "keyboard";
    keyboard.silverlightCtrl = slCtl;
}

jQuery(document).ready(function($) {
    keyboard.Select($("#textBoxInput")[0]);
    $("#checkBoxEachKey").each(function() {
        var current = this;
        current.checked = keyboard.handleEachClick;
        this.onclick = function(event) {
            keyboard.handleEachClick = current.checked;
        }
    });
    $("#checkBoxShowToobar").each(function() {
        var current = this;
        current.checked = keyboard.visibleToolbar;
        this.onclick = function(event) {
            keyboard.visibleToolbar = current.checked;
            if (keyboard.silverlightCtrl != null) {
                keyboard.silverlightCtrl.Content.silverKeyboard.VisibleHelperControls = keyboard.visibleToolbar;
            }

        }
    });

});