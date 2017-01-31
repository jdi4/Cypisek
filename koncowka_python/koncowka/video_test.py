import os
import wx
import os, sys
import MplayerCtrl as mpc


class Frame(wx.Frame):
    
    #----------------------------------------------------------------------
    def __init__(self,parent):
        
        mplayerPath=r'C:\Python27\mplayer\mplayer.exe'
        wx.Frame.__init__(self,parent,1,"TestVideo")
        self.panel = wx.Panel(self)

        self.mpc = mpc.MplayerCtrl(self.panel, -1, mplayerPath)
        self.mpc.Start("bluevideo.mp4")
        mainSizer = wx.BoxSizer(wx.VERTICAL)

        mainSizer.Add(self.mpc, 1, wx.ALL|wx.EXPAND, 5)
        self.panel.SetSizer(mainSizer)
        
        
        self.Show()
        self.panel.Layout()


    

#----------------------------------------------------------------------
if __name__ == "__main__":

            
    app = wx.App(redirect=False)
    frame = Frame(None)
    app.MainLoop()
