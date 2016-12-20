from requests import Session
from requests.auth import HTTPBasicAuth
from signalr import Connection
import xml.etree.cElementTree as ET
import random
import sys
import urllib2
import wx
import time
from pydoc import doc
from _elementtree import tostring


class Bunch:
    def __init__(self,**kwds):
        self.__dict__.update(kwds)
        

def setID():
    ID='ID'
    for x in range (0,16):
        ID = ID+str(random.randrange(0, 9))
    return ID
import wx


     
class Panel1(wx.Panel):
    def changeImage(self,imagename):
        img = wx.Image(imagename)
        self.image = wx.StaticBitmap(self.panel, wx.ID_ANY, wx.BitmapFromImage(img))
        
    def __init__(self):
        # create the panel
        wx.Panel.__init__(self)
        
        self.frame = wx.Frame(None,title="Dislay")
        self.panel = wx.Panel(self.frame)
       
        try:
         
            bmp1 = wx.Image("Rumcajs.jpg",wx.BITMAP_TYPE_ANY).ConvertToBitmap()
            # image's upper left corner anchors at panel 
            # coordinates (0, 0)
            self.image = wx.StaticBitmap(self.panel, -1, bmp1, (0, 0))
            # show some image details
            str1 = "%s  %dx%d" % (image_file, bmp1.GetWidth(),bmp1.GetHeight()) 
            '''parent.SetTitle(str1)'''
            self.panel.Layout()
            self.frame.Show(True)  
        except IOError:
            print "Image file %s not found" % imageFile
            raise SystemExit
        
       
config=Bunch(ID=setID())
lista1=[]
lista1.append({"name":"testname.jpg","time":10})
lista1.append({"name1":"testname1.jpg","time":10})
lista1.append({"name2":"testname2.jpg","time":10})
harm1=Bunch()


 

def internetCheck():
    try :
        url = "http://cypisek.azurewebsites.net"
        urllib2.urlopen(url);
        print"Connected"
        return 1
    except :
        print "Not connect"
        return 0
    
def setSchedule():

    pliki = ['aaa.jpg','bbb.jpg','ccc.jpg']
    czas_pliki=[10,10,20]
    config = ET.Element("config")
    schedule = ET.Element("schedule")
    ET.SubElement(config,"address").text="http://cypisek.azurewebsites.net"
    ET.SubElement(config,"ID").text="123456"
    ET.SubElement(config,"schedule").text="testowy harmonogram"
    ET.SubElement(config,"image").text="pliki[0]";
    ET.SubElement(config,"image").text=pliki[1];
    ET.SubElement(config,"image").text=pliki[2];
    tree=ET.ElementTree(config)
    tree.write("config.xml")
harmString="TestowyHarm,3,obraz1.jpg,10,obraz2.jpg,10,obraz3.jpg,10" 
def getSchedule(harmString):
    words=harmString.split(",")
    w=int(words[1])
    arg=2;
    while (--w>=0): 
        lista1.append({"name":words[arg],"time":words[arg+1]})
        arg+=2
    harm1=Bunch(schedule_name=words[0],playlist=lista1)
def downloadImages():
    list=harm1.playlist;
    count=len(list.name)-1
    i=0
    while i<count:
        print "Download "+list.name[i]
        getImage(list.name[i])
        i=i+1
        
def playHarm():
    list=harm1.playlist;
    count=len(list.name)-1
    i=0
    while True:
        print "Display "+list.name[i]+" for "+list.time[i]+" second."
        setImage(list.name[i])
        time.sleep(list.time[i])
        i=(i+1)%count
        
    
def getImage(file_name,url):
    #url="http://cypisek.azurewebsites.net/storage/images/"+str(file_name)
    print( 'Pobrano zdjecie:', url)
    file_name= file_name+'.jpg'
    with open(file_name,'wb') as f:
        f.write(urllib2.urlopen(url).read())
        f.close()
    setImage(file_name)  
      
def setImage(name):
    print( 'Set image:', name)
    name=name+'.jpg'

    Panel1.changeImage(name)
  
    #panel1.Load("Rumcajs.jpg","Rumcajs.jpg")
    
   
      
    # create a window/frame, no parent, -1 is default ID
    # change the size of the frame to fit the backgound images

#                             
def main():
    if (internetCheck()): 
        
        with Session() as session:
            connection = Connection("http://cypisek.azurewebsites.net/signalr", session)
            chat = connection.register_hub('helloTestHub') #ContentHub
            file_name=''
            
          # print test;
            chat.client.on('setImage', getImage) 
            #chat.client.on('setImage', setImage)  
            #wysylanie ID koncowki
            #chat.server.invoke('SendID',config.ID)
            
            with connection:
              connection.wait(100)  
    else:
        getSchedule()
        #wczytanie pliku xml
            
if __name__ == "__main__":
    #insta=super(Panel1, self).__init__()
   
    app = wx.App()
    #instance for app
   
    main()
    app.MainLoop()
