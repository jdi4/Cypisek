from requests import Session
from requests.auth import HTTPBasicAuth
from signalr import Connection
import xml.etree.cElementTree as ET
import random
import sys
import os
import urllib2
import wx
import time
import glob
from pydoc import doc
from _elementtree import tostring
from wx.lib.pubsub import setuparg1
from wx.lib.pubsub import pub as Publisher


class Bunch:
    def __init__(self,**kwds):
        self.__dict__.update(kwds)
        

def setID():
    ID='ID'
    for x in range (0,16):
        ID = ID+str(random.randrange(0, 9))
    return ID
import wx


     
class ViewerPanel(wx.Panel):
    """"""

    #----------------------------------------------------------------------
    def __init__(self, parent):
        """Constructor"""
        wx.Panel.__init__(self, parent)
        
        width, height = wx.DisplaySize()
        self.picPaths = []
        self.currentPicture = 0
        self.totalPictures = 0
        self.photoMaxSize = height - 200
        Publisher.subscribe(self.updateImages, ("update images"))

        self.slideTimer = wx.Timer(None)
        self.slideTimer.Bind(wx.EVT_TIMER, self.update)
        
        self.layout()
        
    #----------------------------------------------------------------------
    def layout(self):
        """
        Layout the widgets on the panel
        """
        
        self.mainSizer = wx.BoxSizer(wx.VERTICAL)
        btnSizer = wx.BoxSizer(wx.HORIZONTAL)
        
        img = wx.EmptyImage(self.photoMaxSize,self.photoMaxSize)
        self.imageCtrl = wx.StaticBitmap(self, wx.ID_ANY, 
                                         wx.BitmapFromImage(img))
        self.mainSizer.Add(self.imageCtrl, 0, wx.ALL|wx.CENTER, 5)
        self.imageLabel = wx.StaticText(self, label="")
        self.mainSizer.Add(self.imageLabel, 0, wx.ALL|wx.CENTER, 5)
        
        btnData = [("Previous", btnSizer, self.onPrevious),
                   ("Slide Show", btnSizer, self.onSlideShow),
                   ("Next", btnSizer, self.onNext)]
        for data in btnData:
            label, sizer, handler = data
           # self.btnBuilder(label, sizer, handler)
            
        self.mainSizer.Add(btnSizer, 0, wx.CENTER)
        self.SetSizer(self.mainSizer)
            
    #----------------------------------------------------------------------
    def btnBuilder(self, label, sizer, handler):
        """
        Builds a button, binds it to an event handler and adds it to a sizer
        """
        btn = wx.Button(self, label=label)
        btn.Bind(wx.EVT_BUTTON, handler)
        sizer.Add(btn, 0, wx.ALL|wx.CENTER, 5)
        
    #----------------------------------------------------------------------
    def loadImage(self, image):
        """"""
        image_name = os.path.basename(image)
        img = wx.Image(image, wx.BITMAP_TYPE_ANY)
        # scale the image, preserving the aspect ratio
        W = img.GetWidth()
        H = img.GetHeight()
        if W > H:
            NewW = self.photoMaxSize
            NewH = self.photoMaxSize * H / W
        else:
            NewH = self.photoMaxSize
            NewW = self.photoMaxSize * W / H
        img = img.Scale(NewW,NewH)

        self.imageCtrl.SetBitmap(wx.BitmapFromImage(img))
        self.imageLabel.SetLabel(image_name)
        self.Refresh()
        Publisher.sendMessage("resize", "")
        
    #----------------------------------------------------------------------
    def nextPicture(self):
        """
        Loads the next picture in the directory
        """
        if self.currentPicture == self.totalPictures-1:
            self.currentPicture = 0
        else:
            self.currentPicture += 1
        self.loadImage(self.picPaths[self.currentPicture])
        
    #----------------------------------------------------------------------
    def previousPicture(self):
        """
        Displays the previous picture in the directory
        """
        if self.currentPicture == 0:
            self.currentPicture = self.totalPictures - 1
        else:
            self.currentPicture -= 1
        self.loadImage(self.picPaths[self.currentPicture])
        
    #----------------------------------------------------------------------
    def update(self, event):
        """
        Called when the slideTimer's timer event fires. Loads the next
        picture from the folder by calling th nextPicture method
        """
        self.nextPicture()
        
    #----------------------------------------------------------------------
    def updateImages(self, msg):
        """
        Updates the picPaths list to contain the current folder's images
        """
        self.picPaths = msg.data
        self.totalPictures = len(self.picPaths)
        self.loadImage(self.picPaths[0])
        
    #----------------------------------------------------------------------
    def onNext(self, event):
        """
        Calls the nextPicture method
        """
        self.nextPicture()
    
    #----------------------------------------------------------------------
    def onPrevious(self, event):
        """
        Calls the previousPicture method
        """
        self.previousPicture()
    
    #----------------------------------------------------------------------
    def onSlideShow(self, event):
        """
        Starts and stops the slideshow
        """
        btn = event.GetEventObject()
        label = btn.GetLabel()
        if label == "Slide Show":
            self.slideTimer.Start(3000)
            btn.SetLabel("Stop")
        else:
            self.slideTimer.Stop()
            btn.SetLabel("Slide Show")
                
class ViewerFrame(wx.Frame):
    """"""

    #----------------------------------------------------------------------
    def __init__(self):
        """Constructor"""
        wx.Frame.__init__(self, None, title="Koncowka")
        panel = ViewerPanel(self)
        self.folderPath = ""
        Publisher.subscribe(self.resizeFrame, ("resize"))
        
        #self.initToolbar()
        self.sizer = wx.BoxSizer(wx.VERTICAL)
        self.sizer.Add(panel, 1, wx.EXPAND)
        self.SetSizer(self.sizer)
        
        self.Show()
        self.sizer.Fit(self)
        self.Center()  
        
    def PicInit(self):
        folderPath='.'
        picPaths=glob.glob(self.folderPath + "\\*.jpg")
        Publisher.sendMessage("update images", picPaths)
    def resizeFrame(self, msg):
        """"""
        self.sizer.Fit(self)        
             
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

  
    #panel1.Load("Rumcajs.jpg","Rumcajs.jpg")
    
   
      
    # create a window/frame, no parent, -1 is default ID
    # change the size of the frame to fit the backgound images

#                             
def main():
    if (internetCheck()): 
        
            ''' with Session() as session:
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
             '''  
    else:
        getSchedule()
        #wczytanie pliku xml
            
if __name__ == "__main__":
    #insta=super(Panel1, self).__init__()

    
    app = wx.App()
    frame = ViewerFrame()
    
    app.MainLoop()
    main()