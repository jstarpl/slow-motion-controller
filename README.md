Slow Motion Controller for CasparCG MAV/EVS Edition
=====

This is a basic, three-channel slow motion replay controller for CasparCG MAV/EVS Edition.

You can find a sample casparcg.config file in the root directory of this repositiory. It contains:
* 3 input channels (channels 1-3),
* one output channel (channel 4)
* one multiviewer channel (channel 5).

You will need to reconfigure these inputs accordingly. Leave input channels "consumers" tag empty,
it will be populated on connection by the client - only change the output (channel 4) and MV
(channel 5) consumers with whatever you need or want.

For the MultiViewer tally to work, you need to copy the contents of the "media" folder into your
CasparCG "media" folder. You can customize the multiviewer overlay, it's just generic
PNG files.

The software is distributed under GPLv3 - see LICENSE.md

It's written in C# (.NET 4). You will need Visual Studio Express 2010 or later to compile it.
It's functional, but still needs some work. If you would like to contribute,
please contact me, Jan Starzak at <jan.starzak@gmail.com>. Thanks!