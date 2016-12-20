from requests import Session
from requests.auth import HTTPBasicAuth
from signalr import Connection
from xml.dom.minidom import *

import sys
import urllib2
import wx
from pydoc import doc

def write_to_file(doc, name="config.xml"):
    file_object = open(name, "w")
    xml.dom.ext.PrettyPrint(doc, file_object)
    file_object.close()

def make_xml():
    doc= Document()
    node=doc.createElement("foo")
    node.appendChild(doc.createTextNode("test"))
    doc.appendChild(node)
    
    return doc
make_xml().writexml(sys.stdout)
write_to_file(doc)

class Panel1(wx.Panel):
   
    def __init__(self, parent, id):
        # create the panel
        wx.Panel.__init__(self, parent, id)
        

    def Load(self,image_file,image_name):
        try:
            # pick an image file you have in the working 
            # folder you can load .jpg  .png  .bmp  or 
            # .gif files
           
            bmp1 = wx.Image(image_file,wx.BITMAP_TYPE_ANY).ConvertToBitmap()
            # image's upper left corner anchors at panel 
            # coordinates (0, 0)
            self.bitmap1 = wx.StaticBitmap(self, -1, bmp1, (0, 0))
            # show some image details
            str1 = "%s  %dx%d" % (image_file, bmp1.GetWidth(),bmp1.GetHeight()) 
            '''parent.SetTitle(str1)'''
        except IOError:
            print "Image file %s not found" % imageFile
            raise SystemExit
 
       
 
app = wx.App(False)
frame1 = wx.Frame(None, -1, "An image on a panel", 
size=(400, 400))
frame1.Show(True)             
# create the class instance
panel1 = Panel1(frame1, -1)
 
def internetCheck():
    try :
        url = "http://cypisek.azurewebsites.net"
        urllib2.urlopen(url);
        print"Connected"
        return 1
    except :
        print "Not connect"
        return 0
    
 
def main():
    if (internetCheck()): 
        with Session() as session:
            
            try:
                connection = Connection("http://cypisek.azurewebsites.net/signalr", session)
               
            except IOError:
                print "Working without Internet Connection"
            chat = connection.register_hub('helloTestHub')
            file_name=''
            def setImage(file_name, url):
                print('udalo sie')
                print( 'ustawiono zdjecie:', file_name, url)
                
                file_name+='.jpg'
                with open(file_name,'wb') as f:
                    f.write(urllib2.urlopen(url).read())
                    f.close()
                    panel1.Load(file_name, "test")
                # create a window/frame, no parent, -1 is default ID
                # change the size of the frame to fit the backgound images
    
                
                app.MainLoop()
    
            chat.client.on('setImage', setImage)  
            chat.client.on('setImage', setImage)  
            with connection:
              connection.wait(10)  
    

if __name__ == "__main__":
    main()
    
    
    