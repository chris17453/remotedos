using iPlugin;

namespace dmRDClient_PC{
    public class Launcher : iPlugin.iPlugin{
        public void Launch(string[] args) {
            ui u=new ui(args);
            u.ShowDialog();
        }
    }
}
