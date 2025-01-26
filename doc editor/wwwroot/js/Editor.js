
const textarea = document.getElementById("editor");
let lastContent = ""; 

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/documenthub")
    .build();

connection.start().then(() => console.log("Connected to SignalR"));




connection.on("ReceiveDocumentDelta", (position, change, type) => {

    const cursorPositionStart = textarea.selectionStart;
    const cursorPositionEnd = textarea.selectionEnd;

    const content = textarea.value;
    const updatedContent = applyChange(content, position, change, type);

    textarea.value = updatedContent;
    lastContent = updatedContent;
    if (position < cursorPositionStart) {
        textarea.setSelectionRange(cursorPositionStart+1, cursorPositionEnd+1);

    }
    else {
        textarea.setSelectionRange(cursorPositionStart, cursorPositionEnd);
    }
  
});


textarea.addEventListener("input", (e) => {
    const newContent = e.target.value;

   
    const diff = findChange(lastContent, newContent);
    lastContent = newContent;

    var user = document.getElementById("username");
   
    if (diff) {
        connection.invoke("UpdateDocumentDelta", user.textContent, diff.position, diff.change, diff.type);
    }
});


function findChange(oldText, newText) {
    const oldLength = oldText.length;
    const newLength = newText.length;

    let position = 0;

    while (position < oldLength && position < newLength && oldText[position] === newText[position]) {
        position++;
    }

    if (oldLength < newLength) {

        const change = newText.slice(position, position+1);
        return { position, change, type: "insert" };
    }
    else if (oldLength > newLength) {

        const change = oldText.slice(position, oldLength);
        return { position, change, type: "delete" };
    }

    return null; 
}


function applyChange(content, position, change, type) {
    

    if (type === "insert") {
        return content.slice(0, position) + change + content.slice(position);
    }
    else if (type === "delete") {
        return content.slice(0, position) + content.slice(position + 1);
    }
    return content;
    
}