namespace apbd_12_cw2.Interfaces;

public interface IHazardNotifier
{
    void NotifyHazard(string serialNumber, string message);
}