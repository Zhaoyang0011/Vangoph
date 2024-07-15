//drag and drop
const dropArea = document.querySelector(".drag-image"),
  dragText = dropArea.querySelector("h6"),
  button = dropArea.querySelector("button"),
  input = dropArea.querySelector("input"),
  replaceArea = dropArea.querySelector("span");
let file;

button.onclick = (event) => {
  event.preventDefault();
  input.click();
};

input.addEventListener("change", function () {
  file = this.files[0];
  viewfile();
});

dropArea.addEventListener("dragover", (event) => {
  event.preventDefault();
  dragText.textContent = "Release to Upload File";
});

dropArea.addEventListener("dragleave", () => {
  dragText.textContent = "Drag & Drop to Upload File";
});

dropArea.addEventListener("drop", (event) => {
  event.preventDefault();
  file = event.dataTransfer.files[0];
  viewfile();
});

//Examine File Type and Display Image
function viewfile() {
  let fileType = file.type;
  let validExtensions = ["image/jpeg", "image/jpg", "image/png"];
  if (validExtensions.includes(fileType)) {
    let fileReader = new FileReader();
    fileReader.onload = () => {
      let fileURL = fileReader.result;
      let imgTag = `<img src="${fileURL}" alt="image">`;
      replaceArea.innerHTML = imgTag;

      //switch to image mode display
      button.classList.add("imageMode-button");
      dropArea.classList.add("imageMode");
      replaceArea.style.width = "100%";
      replaceArea.style.height = "100%";
    };
    fileReader.readAsDataURL(file);
  } else {
    alert("This is not an Image File!");
    dragText.textContent = "Drag & Drop to Upload File";
  }
}
