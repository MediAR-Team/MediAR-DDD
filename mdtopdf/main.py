import os
from md2pdf.core import md2pdf
from fastapi import FastAPI
from pydantic import BaseModel
from fastapi.responses import FileResponse
import tempfile
from starlette.background import BackgroundTask

app = FastAPI()

class MdToPdfRequest(BaseModel):
  data: str

@app.post('/api/md2pdf')
async def md_to_pdf(request: MdToPdfRequest):
  _, file_name = tempfile.mkstemp(suffix='.pdf')

  def cleanup():
    os.remove(file_name)
  
  md2pdf(file_name, md_content=request.data)

  return FileResponse(file_name, background=BackgroundTask(cleanup))
