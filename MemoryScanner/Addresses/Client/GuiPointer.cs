﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryScanner.Addresses
{
  public class GuiPointer:GetAddresses
    {
        MemoryScanner memScan;
        MemoryReader memRead;
        AddressType Type;
        private int m_address;

        public GuiPointer(MemoryReader _memRead, MemoryScanner _memScan, AddressType _type)
        {
            this.memRead = _memRead;
            this.memScan = _memScan;
            this.Type = _type;
        }
        public override GetAddresses.AddressType AddressCategory
        {
            get
            {
                return Type;
            }

        }
        public override int Address
        {
            get
            {
                return m_address;
            }
            set
            {
                m_address = value;
            }
        }
        public override void Search()
        {
            byte[] SearchBytes = new byte[] {0xC1,0xE8,0x07,0xA8,0x01,0x74,0x0F};
            List<int> values = memScan.ScanBytes(SearchBytes);
            if (values.Count > 0)
            {
                int adr = values[0] - 26;
                m_address = memRead.ReadInt32(adr);
            }
        }
        public override string GetString()
        {
            int val = 0;
            if (m_address == 0)
            {
                Search();
            }
            if (!Util.GlobalVars.ShowWithBase)
            {
                val = Address - memScan.BaseAddress;
            }
            else
            {
                val = Address;
            }    
            return "GuiPointer = 0x" + val.ToString("X");
        }
        public override bool CheckAddress()
        {
            return base.CheckAddress();
        }
    }
}