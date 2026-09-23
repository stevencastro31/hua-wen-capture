import os
os.environ["ARGOS_CHUNK_TYPE"] = "MINISBD"  # Avoid Stanza Chinese sentence-tokenization model from Hugging Face

import argostranslate.package
import argostranslate.translate
FROM_CODE = "zh"
TO_CODE = "en"

# Install Translation Model
MODEL_PATH = os.path.join(os.path.dirname(__file__),"translate-zh_en.argosmodel")
argostranslate.package.install_from_path(MODEL_PATH)

def translate(text: str) -> str:
    return argostranslate.translate.translate(text, FROM_CODE, TO_CODE)