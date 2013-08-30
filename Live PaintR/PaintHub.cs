using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Drawing;
using Newtonsoft.Json;

namespace Live_PaintR
{
    public class PaintHub : Hub
    {
        private static readonly ConcurrentStack<Stroke> _strokes = new ConcurrentStack<Stroke>();

        public void SendDrawing(Stroke stroke)
        {
            _strokes.Push(stroke);
            Clients.AllExcept(Context.ConnectionId).broadcastDrawing(stroke);
        }

        public void ClearDrawing()
        {
            _strokes.Clear();
            Clients.All.broadcastClear();
        }

        public IEnumerable<Stroke> RequestFullDrawing()
        {
            return _strokes.ToArray();
        }
    }
    public class Stroke
    {
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("points")]
        public int[,] Points { get; set; }
    }
}