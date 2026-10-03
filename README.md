# 华文 Capture 
A screen capture tool for translating Chinese text locally.

## About
HuaWenCapture is a desktop screenshot-based Chinese OCR and translation tool. It uses computer vision to process captured screenshots and extract Chinese text, then passes the extracted text to a Neural Machine Translation (NMT) model for local translation.

## Features
- Screen capture using a customizable hotkey.
- Chinese text OCR.
- Browse a local Chinese dictionary from [CC-CEDICT](https://www.mdbg.net/chinese/dictionary?page=cc-cedict).
- Local Chinese-to-English translation using an NMT model.

## Demo
<img width="1200" height="600" alt="HuaWenCapture_sqvvcCz3sm" src="https://github.com/user-attachments/assets/65090bd6-16e8-4b3e-a81b-2bcd1717fb4a" />

<img width="1200" height="610" alt="notepad++_qqSrMq9PoB" src="https://github.com/user-attachments/assets/419ee940-7a9f-404e-b8ca-b32361b6df75" />

<img width="1200" height="568" alt="notepad++_SBaei7aILH" src="https://github.com/user-attachments/assets/349ac701-3c70-4ceb-ad57-5ac3954cd5cb" />

<img width="1200" height="548" alt="notepad++_w6QZ7rAaWG" src="https://github.com/user-attachments/assets/65db62d0-26f3-4d78-93a8-eb8ca6dfd520" />



## Attribution
This project uses models from [Opus-MT](https://github.com/Helsinki-NLP/Opus-MT), converted to ONNX format by [Xenova](https://huggingface.co/Xenova/opus-mt-zh-en), for local Chinese-to-English translation.

## License
This project is licensed under the [MIT License](https://opensource.org/license/MIT)
