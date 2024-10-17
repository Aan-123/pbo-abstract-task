using System;
public class Program
{
    public static void Main()
    {
        Robot robot1 = new RobotZXZ("Robot1", 100, 10, 10);
        BosRobot bos = new BosRobot("Bos", 200, 20, 30, 25);

        IKemampuan perbaikan = new Perbaikan();
        robot1.GunakanKemampuan(perbaikan);

        IKemampuan seranganListrik = new SeranganListrik();
        bos.GunakanKemampuan(seranganListrik);

        IKemampuan seranganPlasma = new SeranganPlasma();
        robot1.GunakanKemampuan(seranganPlasma);

        bos.Diserang(robot1);
        bos.CetakInformasi();
    }
}
public class RobotZXZ : Robot
{
    public RobotZXZ(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan)
    {
    }

    public override void GunakanKemampuan(IKemampuan kemampuan)
    {
        kemampuan.Gunakan(this);
    }
}
public abstract class Robot
{
    public string Nama { get; set; }
    public int Energi { get; set; }
    public int Armor { get; set; }
    public int Serangan { get; set; }

    public Robot(string nama, int energi, int armor, int serangan)
    {
        Nama = nama;
        Energi = energi;
        Armor = armor;
        Serangan = serangan;
    }

    public void Serang(Robot target)
    {
        int damage = Serangan - target.Armor;
        if (damage < 0)
        {
            damage = 0;

        }
        Console.WriteLine($"{Nama} menyerang {target.Nama} dengan {damage} damage.");
        if (damage > 0)
        {
            target.Energi -= damage;
        }
        Console.WriteLine($"{Nama} menyerang {target.Nama} dengan {damage} damage.");
    }

    public abstract void GunakanKemampuan(IKemampuan kemampuan);

    public void CetakInformasi()
    {
        Console.WriteLine($"Nama: {Nama}, Energi: {Energi}, Armor: {Armor}, Serangan: {Serangan}");
    }
}
public interface IKemampuan
{
    void Gunakan(Robot target);
    bool IsCooldown { get; }
}
public class Perbaikan : IKemampuan
{
    public bool IsCooldown { get; private set; }

    public void Gunakan(Robot target)
    {
        if (!IsCooldown)
        {
            target.Energi += 20;
            IsCooldown = true;
            Console.WriteLine($"{target.Nama} menggunakan Perbaikan, energi bertambah 20.");
        }
        else
        {
            Console.WriteLine("Perbaikan sedang cooldown.");
        }
    }
}
public class SeranganListrik : IKemampuan
{
    public bool IsCooldown { get; private set; }

    public void Gunakan(Robot target)
    {
        if (!IsCooldown)
        {
            target.Energi -= 30;
            target.Armor = 0;
            IsCooldown = true;
            Console.WriteLine($"{target.Nama} terkena Serangan Listrik, energi berkurang 30.");

        }
        else
        {
            Console.WriteLine("Serangan Listrik sedang cooldown.");
        }
    }
}

public class SeranganPlasma : IKemampuan
{
    public bool IsCooldown { get; private set; }

    public void Gunakan(Robot target)
    {
        if (!IsCooldown)
        {
            target.Energi -= 25;
            IsCooldown = true;
            Console.WriteLine($"{target.Nama} terkena Serangan Plasma, energi berkurang 25.");
        }
        else
        {
            Console.WriteLine("Serangan Plasma sedang cooldown.");
        }
    }
}
public class BosRobot : Robot
{
    public int Pertahanan { get; set; }

    public BosRobot(string nama, int energi, int armor, int serangan, int pertahanan) : base(nama, energi, armor, serangan)
    {
        Pertahanan = pertahanan;
        Armor += Pertahanan;
    }

    public void Diserang(Robot penyerang)
    {
        int damage = penyerang.Serangan - Armor;
        if (damage < 0)
        {
            damage = 0;
        }
        if (damage > 0)
        {
            Energi -= damage;
        }
        Console.WriteLine($"{Nama} diserang oleh {penyerang.Nama} dengan {damage} damage.");
        if (Energi <= 0)
        {
            Mati();
        }
    }

    public void Mati()
    {
        Console.WriteLine($"{Nama} telah mati.");
    }

    public override void GunakanKemampuan(IKemampuan kemampuan)
    {
        kemampuan.Gunakan(this);
    }
}