import glob
import os
import wx
import urllib2
import random
from wx.lib.pubsub import setuparg1
from wx.lib.pubsub import pub as Publisher
from xml.etree.ElementTree import parse, Element 


picList=["rumcajs.jpg","danusia1.jpg"]

class ViewClass(wx.Panel):
    def __init__(self, parent):
         
        wx.Panel.__init__(self, parent)
        self.currentPicture = 0
        self.totalPictures = 0
        width, height = wx.DisplaySize()
        self.photoMaxSize = height - 200
        #self.picPaths = picList
        self.picTime = []
       # loadImage()
        Publisher.subscribe(self.loadImage, ("load"))
        # layout definition
        self.mainSizer = wx.BoxSizer(wx.VERTICAL)
        img = wx.EmptyImage(self.photoMaxSize,self.photoMaxSize)
        self.imageCtrl = wx.StaticBitmap(self, wx.ID_ANY, 
                                         wx.BitmapFromImage(img))
        
        self.mainSizer.Add(self.imageCtrl, 0, wx.ALL|wx.CENTER, 5)
        
        
        self.slideTimer = wx.Timer(None)
        self.slideTimer.Bind(wx.EVT_TIMER, self.update)
        self.ScheduleTimer(3000)
        
    def loadImage(self, msg):
        
        print "wyswietlam: "
        image=msg.data
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
        #self.imageLabel.SetLabel(image_name)
        self.Refresh()
        Publisher.sendMessage("resize", "")
    def NextPic(self):
        if self.currentPicture == self.totalPictures-1:
            self.currentPicture = 0
        else:
            self.currentPicture += 1
        self.loadImage(self.picPaths[self.currentPicture]) 
        
    def ScheduleTimer(self, time):
        """
        Starts and stops the slideshow
        """
        #po skonczeniu odliczania wywoluje metode update
        self.slideTimer.Start(time)
            
    def update(self, event):
        print "Timer over"
    def test(self):
        print "test"     

class ViewerFrame(wx.Frame):
     def __init__(self):
        """Constructor"""
        wx.Frame.__init__(self, None, title="Timer test")
        panel = ViewClass(self)
        #self.folderPath = ""
        Publisher.subscribe(self.resizeFrame, ("resize"))
        
        self.sizer = wx.BoxSizer(wx.VERTICAL)
        self.sizer.Add(panel, 1, wx.EXPAND)
        self.SetSizer(self.sizer)
        
        self.Show()
        self.sizer.Fit(self)
        self.Center()
        
     def resizeFrame(self, msg):
        """"""
        self.sizer.Fit(self)
        
class Bunch:
    def __init__(self,**kwds):
        self.__dict__.update(kwds)
    
class main():
    def __init__(self):
        #globaly obcject to keep schedule informtion
        self.harm = Bunch() 
        harmString="Harmonogram1,8,chalets_2.jpg,10,chalets_3.jpg,10,chalets_4.jpg,10,chalets_big.jpg,10,zoom1.jpg,10,zoom2.jpg,10,zoom3.jpg,10,zoom4.jpg,10"   
        #harmString="TestowyHarm,3,obraz1.jpg,10,obraz2.jpg,10,obraz3.jpg,10" 
        url= "https://pbs.twimg.com/profile_images/580157476512739328/N2VXzbVN.jpg"
        url2="https://thumbs.dreamstime.com/z/pretty-girl-cup-hot-tea-winter-forest-43545393.jpg"
        print "pobranie i ustawienie zdjecia1"
        self.getImage("chalets_2")

        #Publisher.sendMessage("update images", picPaths)
         
        self.getSchedule(harmString)
        print "ID to "+self.harm.ID; 
        print self.harm.playlist[1];  
    def getImage(self,file_name):
        #pic_path="http://cypisek.azurewebsites.net/storage/images/"+str(file_name)
        pic_path="http://www.camping-oliviers-porto.com/files/jpg/chalets_luxes_pages/"
        url=pic_path+str(file_name)
        print( 'Pobrano zdjecie:', url)
       # file_name= file_name+'.jpg'
        with open(file_name,'wb') as f:
            f.write(urllib2.urlopen(url).read())
            f.close()
        
        Publisher.sendMessage("load", file_name)   
        print "zaladowano "+str(file_name)
        
    def setID(self):
        ID='ID'
        for x in range (0,16):
            ID = ID+str(random.randrange(0, 9))
        return ID      
    def internetCheck(self):
        try :
            url = "http://cypisek.azurewebsites.net"
            urllib2.urlopen(url);
            print"Connected"
            return 1
        except :
            print "Not connect"
            return 0         
    def getSchedule(self,harmString):
        lista=[]
        words=harmString.split(",")
        w=int(words[1])
        arg=2;
        while (w): 
            lista.append({"name":words[arg],"time":words[arg+1]})
            arg+=2
            w=w-1
        self.harm=Bunch(schedule_name=words[0],ID=self.setID(),playlist=lista,) 
        
if __name__ == "__main__":
    
    app = wx.App()
    frame = ViewerFrame()
    main()
    app.MainLoop()
        